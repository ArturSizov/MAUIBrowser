namespace MAUIBrowser.Abstractions
{
    /// <summary>
    /// Implementing Tab View
    /// </summary>
    public interface ITabsPopupService
    {
        /// <summary>
        /// Shows tabs
        /// </summary>
        /// <returns></returns>
        Task ShowAsync();

        /// <summary>
        /// Closes a tab
        /// </summary>
        /// <returns></returns>
        Task CloseAsync();
    }
}
