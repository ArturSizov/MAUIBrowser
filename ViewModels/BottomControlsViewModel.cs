using MAUIBrowser.Abstractions;
using MAUIBrowser.Pages;
using MAUIBrowser.State;
using System.Windows.Input;

namespace MAUIBrowser.ViewModels
{
    public class BottomControlsViewModel : BindableObject
    {
        private ITabsPopupService _tabsPopupService;
        private IWebViewService<WebView> _webViewService;
		private IHistoryPopupService _historyPopupService;

        public BrowserState BrowserState { get; }

        public BottomControlsViewModel(BrowserState browserState, ITabsPopupService tabsPopupService, IWebViewService<WebView> webViewService, IHistoryPopupService historyPopupService)
        {
            _tabsPopupService = tabsPopupService;
            _webViewService = webViewService;
            _historyPopupService = historyPopupService;
            BrowserState = browserState;
        }

        /// <summary>
        /// Go Back Web View command
        /// </summary>
        public ICommand GoBackCommand => new Command(() =>
        {
            if (Application.Current?.MainPage is not ContentPage contentPage)
                return;

            _webViewService.GoBack();

        });

        /// <summary>
        ///   Go Forward Web View command
        /// </summary>
        public ICommand GoForwardCommand => new Command(() =>
        {
            if (Application.Current?.MainPage is not ContentPage contentPage)
                return;

           _webViewService.GoForward();
        });

        /// <summary>
        /// Open Home view command 
        /// </summary>
        public ICommand OpenHomeCommand => new Command(() =>
        {
            if (Application.Current?.MainPage is not ContentPage contentPage)
                return;

            contentPage.Content = new HomePanelView();
            BrowserState.CurrentTab = null;
        });

        /// <summary>
        /// Open show tab view command
        /// </summary>
        public ICommand OpenTabsCommand => new Command(async () =>
        {
            if (Application.Current?.MainPage == null)
                return;

            await _tabsPopupService.ShowAsync();
        });

        /// <summary>
        /// Open show settings view command
        /// </summary>
        public ICommand OpenHistoryCommand => new Command(async() =>
        {
            if (Application.Current?.MainPage == null)
                return;

            await _historyPopupService.ShowAsync();
        });
    }
}
