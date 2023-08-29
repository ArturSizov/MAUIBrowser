using MAUIBrowser.Abstractions;
using MAUIBrowser.Pages;
using MAUIBrowser.State;

namespace MAUIBrowser.Services
{
    public class WebViewServices : IWebViewService<WebView>
    {
        private BrowserState _state;

        public WebView WebView { get; set; } = new();

        public WebViewServices(BrowserState state)
        {
            _state = state;
        }

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
                if (_state.CurrentTab != null)
                    _state.Tabs.Remove(_state.CurrentTab);

                _state.CurrentTab = null;
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
    }
}
