using CommunityToolkit.Maui.Views;
using MAUIBrowser.Abstractions;
using MAUIBrowser.Pages;

namespace MAUIBrowser.Services
{
    public class SettingsPopupServices : ISettingsPopupServices
    {
        #region Private property 
        private Popup? popup;

        private bool disposed;

       
        #endregion
        #region Methods 
        /// <summary>
        /// Show popup window
        /// </summary>
        /// <returns></returns>
        public async Task ShowAsync()
        {
            if (Application.Current?.MainPage == null)
                return;

            disposed = false;
            popup = new SettingsView();
            popup.Closed += PopupClosed;

            await Application.Current.MainPage.ShowPopupAsync(popup);
        }

        public Task CloseAsync()
        {
            throw new NotImplementedException();
        }

        // Closed popup window
        private void PopupClosed(object? sender, CommunityToolkit.Maui.Core.PopupClosedEventArgs e)
        {
            if (popup != null)
                popup.Closed -= PopupClosed;

            disposed = true;
        }
        #endregion
    }
}
