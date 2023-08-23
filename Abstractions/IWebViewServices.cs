namespace MAUIBrowser.Abstractions
{
    public interface IWebViewServices
    {
        public WebView WebView { get; set; }
        void GoBack();
        void GoForward();
    }
}
