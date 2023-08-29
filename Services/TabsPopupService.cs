using MAUIBrowser.Abstractions;
using CommunityToolkit.Maui.Views;
using MAUIBrowser.Pages;

namespace MAUIBrowser.Services
{
    public class TabsPopupService : ITabsPopupService
    {
        private Popup? _popup;
        private bool _disposed;

        /// <summary>
        /// Show popup window
        /// </summary>
        /// <returns></returns>
        public async Task ShowAsync()
        {
            if (Application.Current?.MainPage == null)
                return;

            _disposed = false;
            _popup = new TabsCollectionPopup();
            _popup.Closed += PopupClosed;

            await Application.Current.MainPage.ShowPopupAsync(_popup);
        }

        /// <summary>
        /// Closed window
        /// </summary>
        /// <returns></returns>
        public async Task CloseAsync()
        {
            if (_popup == null)
                return;

            if (!_disposed)
                await _popup.CloseAsync();

            _popup = null;
        }


        // Closed popup window
        private void PopupClosed(object? sender, CommunityToolkit.Maui.Core.PopupClosedEventArgs e)
        {
            if (_popup != null)
                _popup.Closed -= PopupClosed;

            _disposed = true;
        }
    }
}
