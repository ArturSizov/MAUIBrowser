using MAUIBrowser.Abstractions;
using CommunityToolkit.Maui.Views;
using MAUIBrowser.Pages;

namespace MAUIBrowser.Services
{
    public class TabsPopupService : ITabsPopupService
    {
        private Popup? popup;

        private bool disposed;

        public async Task ShowAsync()
        {
            if (Application.Current?.MainPage == null)
                return;

            disposed = false;
            popup = new TabsCollectionPage();
            popup.Closed += PopupClosed;

            await Application.Current.MainPage.ShowPopupAsync(popup);
        }

        public async Task CloseAsync()
        {
            if (popup == null)
                return;

            if (!disposed)
                await popup.CloseAsync();

            popup = null;
        }

        private void PopupClosed(object? sender, CommunityToolkit.Maui.Core.PopupClosedEventArgs e)
        {
            if (popup != null)
                popup.Closed -= PopupClosed;

            disposed = true;
        }
    }
}
