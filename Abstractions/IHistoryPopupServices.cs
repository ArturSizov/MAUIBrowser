namespace MAUIBrowser.Abstractions
{
    public interface IHistoryPopupServices
    {
        Task ShowAsync();
        Task CloseAsync();
    }
}
