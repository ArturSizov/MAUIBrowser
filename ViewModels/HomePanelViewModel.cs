using MAUIBrowser.Abstractions;
using MAUIBrowser.Auxiliary;
using MAUIBrowser.Models;
using MAUIBrowser.Pages;
using MAUIBrowser.State;
using System.Windows.Input;

namespace MAUIBrowser.ViewModels
{
    public class HomePanelViewModel : BindableObject
    {
        #region Private property 
        private IWebViewServices web;
        private BrowserState browserState;
        private string url = string.Empty;

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
        #endregion

        public HomePanelViewModel(BrowserState browserState, IWebViewServices web)
        {
            this.web = web;
            this.browserState = browserState;
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
                Url = target ,
                Title = url,
                Content = new BrowserTabPage(web) 
                {
                    BindingContext = new BrowserTabPageModel
                    {
                        Url =  target
                    }
                }
            };

            if (browserState.CurrentTab != null)
            {
                var index = browserState.Tabs.IndexOf(browserState.CurrentTab);

                if (index != -1)
                    browserState.Tabs[index] = tab;
            }

            else
                browserState.Tabs.Add(tab);

            browserState.CurrentTab = tab;

            contentPage.Content = tab.Content;

        });
        #endregion
    }
}
