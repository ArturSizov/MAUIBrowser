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
        private BrowserState browserState;
        private string url = string.Empty;

        #endregion

        #region Public property 
        public string Title => "MAUI Browser";
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

        public HomePanelViewModel(BrowserState browserState)
        {
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
                Url = new UrlWebViewSource() { Url = target },
                Title = url,
                Content = new BrowserTabPage() 
                {
                    BindingContext = new BrowserTabPageModel
                    {
                        Url = new UrlWebViewSource() { Url = target }
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
