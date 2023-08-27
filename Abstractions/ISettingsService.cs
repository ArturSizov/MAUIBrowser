namespace MAUIBrowser.Abstractions
{
    public interface ISettingsService
    {
        Task<T> GetSettings<T>(string key, T defaultValue);
        Task SaveSettings<T>(string key, T value);
    }
}
