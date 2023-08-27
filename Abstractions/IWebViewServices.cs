namespace MAUIBrowser.Abstractions
{
    public interface IWebViewServices
    {
        public WebView WebView { get; set; }
        bool GoBack();
        void GoForward();
    }
}
