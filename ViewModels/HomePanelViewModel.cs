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
        #region Private property 
        private IWebViewService<WebView> web;
        private string url = string.Empty;
        private SearchEngineModel searchEngine;
        private ISettingsService settings;
        private int count;
        private bool isVisibleDeleteFastLink;
        #endregion

        #region Public property 
        public BrowserState BrowserState { get; }
        public string Url
        {
            get => url; 
            set
            {
                url = value;
                OnPropertyChanged();
            }
        }
        public SearchEngineModel SearchEngine
        {
            get => searchEngine;
            set
            {
                searchEngine = value;
                OnPropertyChanged();
            }
        }
        public bool IsVisibleDeleteFastLink
        {
            get => isVisibleDeleteFastLink;
            set
            {
                isVisibleDeleteFastLink = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public HomePanelViewModel(BrowserState state, IWebViewService<WebView> web, ISettingsService settings)
        {
            this.settings = settings;
            this.web = web;
            BrowserState = state;
            Task.Run(async()=> await LoadingAppAsync());
        }

        #region Commands

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
                Content = new BrowserTabPage(web) 
                {
                    BindingContext = new BrowserTabPageModel(BrowserState)
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

                var index = BrowserState.SearchEngineState.SearchEngines.IndexOf(SearchEngine);

                count = index;

                await settings.SaveSettings(nameof(count), count);
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

            await BrowserState.FastLinksState.InsertAsync(new()
            {
                Title = title,
                Url = source
            });

            OnPropertyChanged(nameof(BrowserState));
            OnPropertyChanged(nameof(BrowserState.FastLinksState.Links));
        });

        /// <summary>
        /// Remove one link
        /// </summary>
        public ICommand RemoveLinkCommand => new Command<FastLinkModel>(async(fastLink) =>
        {
            await BrowserState.FastLinksState.RemoveAsync(fastLink);
        });
        #endregion

        #region Methods

        //Content Applications
        private void ApplyContent(string title, string url)
        {
            if (Application.Current?.MainPage is not ContentPage contentPage)
                return;

            var tab = new TabInfoModel
            {
                Url = url,
                Title = title,
                Content = new BrowserTabPage(web)
                {
                    BindingContext = new BrowserTabPageModel(BrowserState)
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

        //Load application
        private async Task LoadingAppAsync()
        {
            count = await settings.GetSettings<int>(nameof(count), 0);

            SearchEngine = BrowserState.SearchEngineState.SearchEngines[count];

            WebViewSourceBuilder.SearchString = SearchEngine.SearchQuery;
        }
        #endregion
    }
}
