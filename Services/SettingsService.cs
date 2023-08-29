using MAUIBrowser.Abstractions;

namespace MAUIBrowser.Services
{
    public class SettingsService : ISettingsService
    {
        public Task<T> GetSettingsAsync<T>(string key, T defaultValue)
        {
            var result = Preferences.Default.Get(key, defaultValue);
            return Task.FromResult(result);
        }

        public Task SaveSettingsAsync<T>(string key, T value)
        {
            Preferences.Default.Set(key, value);
            return Task.CompletedTask;
        }
    }
}
