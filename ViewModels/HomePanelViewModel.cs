using MAUIBrowser.Abstractions;
using MAUIBrowser.Auxiliary;
using MAUIBrowser.Models;
using MAUIBrowser.Pages;
using MAUIBrowser.State;
using System.Windows.Input;
using UraniumUI.Dialogs.Mopups;

namespace MAUIBrowser.ViewModels
{
    public class HomePanelViewModel : BindableObject
    {
        private readonly IBrowserStateManager<HistoryModel> _historyManager;
		private readonly IWebViewService<WebView> _webService;
		private readonly ISettingsService _settingsService;

		private string _url = string.Empty;
        private SearchEngineModel? _searchEngine;
        private int _count;
        private bool _isVisibleDeleteFastLink;

        public BrowserState BrowserState { get; }


		public IBrowserStateManager<FastLinkModel> FastLinksManager { get; }

		public IBrowserStateManager<SearchEngineModel> SearchManager { get; }

		public string Url
        {
            get => _url; 
            set
            {
				_url = value;
                OnPropertyChanged();
            }
        }

        public SearchEngineModel? SearchEngine
        {
            get => _searchEngine;
            set
            {
				_searchEngine = value;
                OnPropertyChanged();
            }
        }

        public bool IsVisibleDeleteFastLink
        {
            get => _isVisibleDeleteFastLink;
            set
            {
                _isVisibleDeleteFastLink = value;
                OnPropertyChanged();
            }
        }

        public HomePanelViewModel(BrowserState state, 
            IBrowserStateManager<HistoryModel> historyManager, 
            IBrowserStateManager<SearchEngineModel> searchManager,
			IBrowserStateManager<FastLinkModel> fastLinksManager,
			IWebViewService<WebView> webService, ISettingsService settingsService)
        {
            _historyManager = historyManager;
            _settingsService = settingsService;
			_webService = webService;
            BrowserState = state;
            FastLinksManager = fastLinksManager;
			SearchManager = searchManager;
			Task.Run(LoadingAppAsync);
        }

        /// <summary>
        /// Enter address command
        /// </summary>
        public ICommand EnterAddressCommand => new Command<string>((url) =>
        {
            if (Application.Current?.MainPage is not ContentPage contentPage)
                return;

            var target = WebViewSourceBuilder.Create(url);

            var tab = new TabInfoModel
            {
                Url = target,
                Title = url,
                Content = new BrowserTabPage(_webService) 
                {
                    BindingContext = new BrowserTabPageModel(_historyManager)
                    {
                        Url =  target,
                        Title = Url,
                        EntryUrl = target
                    }
                }
            };

            if (BrowserState.CurrentTab != null)
            {
                var index = BrowserState.Tabs.IndexOf(BrowserState.CurrentTab);

                if (index != -1)
                    BrowserState.Tabs[index] = tab;
            }

            else
                BrowserState.Tabs.Add(tab);


            BrowserState.CurrentTab = tab;

            contentPage.Content = tab.Content;

        });

        /// <summary>
        /// Postbox Replacement Command
        /// </summary>
        public ICommand InstallASearchSystemCommand => new Command<SearchEngineModel>(async (ser) =>
        {
            if (ser != null)
            {
                SearchEngine = ser;
                WebViewSourceBuilder.SearchString = SearchEngine.SearchQuery;

                var index = SearchManager.Items.IndexOf(SearchEngine);

                _count = index;

                await _settingsService.SaveSettingsAsync(nameof(_count), _count);
            }   
        });

        /// <summary>
        /// Fast link Command
        /// </summary>
        public ICommand FastLinkCommand => new Command<FastLinkModel>((fastLink) =>
        {
            ApplyContent(fastLink.Title, fastLink.Url);
        });

        /// <summary>
        /// Sets the visibility of the delete button
        /// </summary>
        public ICommand ButtonViewPressedCommand => new Command(() =>
        {
            IsVisibleDeleteFastLink = !IsVisibleDeleteFastLink;
        });

        /// <summary>
        /// Add fast link Command
        /// </summary>
        public ICommand AddFastLinkCommand => new Command<FastLinkModel>(async(fastLink) =>
        {
            if (Application.Current?.MainPage is not ContentPage contentPage)
                return;

            IsVisibleDeleteFastLink = false;

            var title = await contentPage.DisplayTextPromptAsync("Пожалуйста, введите данные", "Uri или запрос", "Ok", "Отмена");

            if (string.IsNullOrWhiteSpace(title))
                return;

            var source = WebViewSourceBuilder.Create(title);

            // if user added full uri, use hostname as a title
            if (Uri.TryCreate(title, UriKind.Absolute, out var uri) && uri != null)
                title = uri.Host;

            await FastLinksManager.CreateAsync(new()
            {
                Title = title,
                Url = source
            });

            OnPropertyChanged(nameof(FastLinksManager.Items));
        });

        /// <summary>
        /// Remove one link
        /// </summary>
        public ICommand RemoveLinkCommand => new Command<FastLinkModel>(async(fastLink) =>
        {
            await FastLinksManager.DeleteAsync(fastLink);
        });

		/// <summary>
		/// Applies content
		/// </summary>
		/// <param name="title">Title</param>
		/// <param name="url">Url</param>
		private void ApplyContent(string title, string url)
        {
            if (Application.Current?.MainPage is not ContentPage contentPage)
                return;

            var tab = new TabInfoModel
            {
                Url = url,
                Title = title,
                Content = new BrowserTabPage(_webService)
                {
                    BindingContext = new BrowserTabPageModel(_historyManager)
                    {
                        Url = url,
                        Title = title,
                        EntryUrl = url
                    }
                }
            };

            if (BrowserState.CurrentTab != null)
            {
                var index = BrowserState.Tabs.IndexOf(BrowserState.CurrentTab);

                if (index != -1)
                    BrowserState.Tabs[index] = tab;
            }
            else
                BrowserState.Tabs.Add(tab);

            BrowserState.CurrentTab = tab;

            contentPage.Content = tab.Content;
        }

		/// <summary>
		/// Load application
		/// </summary>
		/// <returns></returns>
		private async Task LoadingAppAsync()
        {
            _count = await _settingsService.GetSettingsAsync(nameof(_count), 0);
            SearchEngine = SearchManager.Items[_count];
            WebViewSourceBuilder.SearchString = SearchEngine.SearchQuery;
        }
    }
}
