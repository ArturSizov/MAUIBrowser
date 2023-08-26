using MAUIBrowser.Abstractions;
using MAUIBrowser.Models;
using MAUIBrowser.Pages;
using MAUIBrowser.State;
using System.Windows.Input;

namespace MAUIBrowser.ViewModels
{
    public class BottomControlsViewModel : BindableObject
    {
        #region Private property 
        private BrowserState browserState = new();
        private ITabsPopupService popupService;
        private IWebViewServices web;
        private IHistoryPopupServices hisPopup;
        #endregion

        #region Public property 
        public BrowserState BrowserState
        {
            get => browserState; 
            set
            {
                browserState = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public BottomControlsViewModel(ITabsPopupService popupService, IWebViewServices web, BrowserState browserState, IHistoryPopupServices hisPopup)
        {
            this.popupService = popupService;
            this.web = web;
            this.hisPopup = hisPopup;
            BrowserState = browserState;
        }

        #region Commands 
        /// <summary>
        /// Go Back Web View command
        /// </summary>
        public ICommand GoBackCommand => new Command(() =>
        {
            if (Application.Current?.MainPage is not ContentPage contentPage)
                return;

            web.GoBack();

        });

        /// <summary>
        ///   Go Forward Web View command
        /// </summary>
        public ICommand GoForwardCommand => new Command(() =>
        {
            if (Application.Current?.MainPage is not ContentPage contentPage)
                return;

           web.GoForward();
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

            await popupService.ShowAsync();
        });

        /// <summary>
        /// Open show settings view command
        /// </summary>
        public ICommand OpenHistoryCommand => new Command(async() =>
        {
            if (Application.Current?.MainPage == null)
                return;

            await hisPopup.ShowAsync();
        });
        #endregion
    }
}
