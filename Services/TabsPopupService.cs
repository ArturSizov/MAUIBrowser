using MAUIBrowser.Abstractions;
using CommunityToolkit.Maui.Views;
using MAUIBrowser.Pages;

namespace MAUIBrowser.Services
{
    public class TabsPopupService : ITabsPopupService
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
            popup = new TabsCollectionPopup();
            popup.Closed += PopupClosed;

            await Application.Current.MainPage.ShowPopupAsync(popup);
        }

        /// <summary>
        /// Closed window
        /// </summary>
        /// <returns></returns>
        public async Task CloseAsync()
        {
            if (popup == null)
                return;

            if (!disposed)
                await popup.CloseAsync();

            popup = null;
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
