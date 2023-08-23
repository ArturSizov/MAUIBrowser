using MAUIBrowser.Abstractions;
using MAUIBrowser.Pages;

namespace MAUIBrowser.Services
{
    public class WebViewServices : IWebViewServices
    {

        #region Public property 
        public WebView WebView { get; set; } = new();
        #endregion

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
            else contentPage.Content = new HomePanelView();
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
