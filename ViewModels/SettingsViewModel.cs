using MAUIBrowser.Abstractions;

namespace MAUIBrowser.ViewModels
{
    public class SettingsViewModel : BindableObject
    {

        #region Private property 
        private ISettingsPopupServices settingsPopup;
        #endregion
        public SettingsViewModel(ISettingsPopupServices settingsPopup)
        {
            this.settingsPopup = settingsPopup;
        }
    }
}
