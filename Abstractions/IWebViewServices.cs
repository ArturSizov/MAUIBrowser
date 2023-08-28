namespace MAUIBrowser.Abstractions
{
    /// <summary>
    /// Web Service Implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IWebViewServices<T>
    {
        public T WebView { get; set; }

        /// <summary>
        /// Go back one page
        /// </summary>
        /// <returns></returns>
        bool GoBack();

        /// <summary>
        /// Go one page forward
        /// </summary>
        void GoForward();
    }
}
