namespace MAUIBrowser.Abstractions
{
    /// <summary>
    /// Implementation of the browsing history window
    /// </summary>
    public interface IHistoryPopupService
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
