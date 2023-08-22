using MAUIBrowser.Abstractions;
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

        public BottomControlsViewModel(ITabsPopupService popupService, BrowserState browserState)
        {
            this.popupService = popupService;
            BrowserState = browserState;
        }

        #region Commands 
        public ICommand GoBackCommand => new Command(async() =>
        {
            if (Application.Current?.MainPage == null)
                return;

            await Shell.Current.DisplayAlert("О программе", "Программа для напоминаний о днях рождений", "Ok");
        });

        public ICommand OpenHomeCommand => new Command(() =>
        {
            if (Application.Current?.MainPage is not ContentPage contentPage)
                return;

            contentPage.Content = new HomePanelView();
            BrowserState.CurrentTab = null;
        });

        public ICommand OpenTabsCommand => new Command(async () =>
        {
            if (Application.Current?.MainPage == null)
                return;

            await popupService.ShowAsync();
        });
        #endregion
    }
}
