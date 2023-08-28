namespace MAUIBrowser.Abstractions
{
    /// <summary>
    /// Implementation of the browsing history window
    /// </summary>
    public interface IHistoryPopupServices
    {
        /// <summary>
        /// Show browsing history
        /// </summary>
        /// <returns></returns>
        Task ShowAsync();
        /// <summary>
        /// Close browsing history
        /// </summary>
        /// <returns></returns>
        Task CloseAsync();
    }
}
