using MAUIBrowser.Abstractions;

namespace MAUIBrowser.Services
{
    public class SettingsService : ISettingsService
    {
        public Task<T> GetSettings<T>(string key, T defaultValue)
        {
            var result = Preferences.Default.Get<T>(key, defaultValue);

            return Task.FromResult(result);
        }

        public Task SaveSettings<T>(string key, T value)
        {
            Preferences.Default.Set(key, value);
            return Task.CompletedTask;
        }
    }
}
