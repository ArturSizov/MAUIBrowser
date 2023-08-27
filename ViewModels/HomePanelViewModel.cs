using MAUIBrowser.Abstractions;
using MAUIBrowser.Auxiliary;
using MAUIBrowser.Models;
using MAUIBrowser.Pages;
using MAUIBrowser.State;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MAUIBrowser.ViewModels
{
    public class HomePanelViewModel : BindableObject
    {
        #region Private property 
        private IWebViewServices web;
        private BrowserState state;
        private string url = string.Empty;
        private SearchEngineModel searchEngine;
        private ISettingsService settings;

        #endregion

        #region Public property 
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

        public ObservableCollection<SearchEngineModel> SearchEngines { get; set; }

        private int count;
        #endregion

        public HomePanelViewModel(BrowserState state, IWebViewServices web, ISettingsService settings)
        {
            this.settings = settings;
            this.web = web;
            this.state = state;
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
                    BindingContext = new BrowserTabPageModel(state)
                    {
                        Url =  target
                    }
                }
            };

            if (state.CurrentTab != null)
            {
                var index = state.Tabs.IndexOf(state.CurrentTab);

                if (index != -1)
                    state.Tabs[index] = tab;
            }

            else
                state.Tabs.Add(tab);


            state.CurrentTab = tab;

            contentPage.Content = tab.Content;

        });

        /// <summary>
        /// 
        /// </summary>
        public ICommand InstallASearchSystemCommand => new Command<SearchEngineModel>(async (ser) =>
        {
            if (ser != null)
            {
                SearchEngine = ser;
                WebViewSourceBuilder.SearchString = SearchEngine.SearchQuery;

                var index = SearchEngines.IndexOf(SearchEngine);

                count = index;

                await settings.SaveSettings(nameof(count), count);
            }   
        });

        private async Task LoadingAppAsync()
        {
            SearchEngines = new ObservableCollection<SearchEngineModel>
            {
                new SearchEngineModel
                {
                    Image = "google.png",
                    SearchQuery = "https://www.google.com/search?q="
                },
                new SearchEngineModel
                {
                    Image = "rambler.png",
                    SearchQuery = "https://nova.rambler.ru/search?query="
                },
                new SearchEngineModel
                {
                    Image = "yandex.png",
                    SearchQuery = "https://ya.ru/search/?text="
                }
            };

            count = await settings.GetSettings<int>(nameof(count), 0);

            SearchEngine = SearchEngines[count];

            WebViewSourceBuilder.SearchString = SearchEngine.SearchQuery;
        }
        #endregion
    }
}
