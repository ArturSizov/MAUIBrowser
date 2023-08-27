using MAUIBrowser.Abstractions;
using MAUIBrowser.Pages;
using MAUIBrowser.State;

namespace MAUIBrowser.Services
{
    public class WebViewServices : IWebViewServices
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
        public void GoBack()
        {
            if (Application.Current?.MainPage is not ContentPage contentPage)
                return;

            if (WebView.CanGoBack)
                WebView.GoBack();
            else
            {
                state.Tabs.Remove(state.CurrentTab);
                state.CurrentTab = null;
                contentPage.Content = new HomePanelView();
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
