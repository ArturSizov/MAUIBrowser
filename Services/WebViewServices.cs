using MAUIBrowser.Abstractions;
using MAUIBrowser.Pages;
using MAUIBrowser.State;

namespace MAUIBrowser.Services
{
    public class WebViewServices : IWebViewService<WebView>
    {
        #region Private property 
        private BrowserState state;

        #endregion

        #region Public property 
        public WebView WebView { get; set; } = new();
        #endregion
        public WebViewServices(BrowserState state)
        {
            this.state = state;
        }
        #region Methods
        /// <summary>
        /// Return to previous page
        /// </summary>
        public bool GoBack()
        {
            if (Application.Current?.MainPage is not ContentPage contentPage)
                return false;

            if (WebView.CanGoBack)
            {
                WebView.GoBack();
                return true;
            }
            else
            {
                state.Tabs.Remove(state.CurrentTab);
                state.CurrentTab = null;
                contentPage.Content = new HomePanelView();
                return false;
            }
        }

        /// <summary>
        /// Page forward
        /// </summary>
        public void GoForward()
        {
           if(WebView.CanGoForward)
                WebView.GoForward();
        }
        #endregion

    }
}
