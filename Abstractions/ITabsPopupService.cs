namespace MAUIBrowser.Abstractions
{
    public interface ITabsPopupService
    {
        Task ShowAsync();
        Task CloseAsync();
    }
}
