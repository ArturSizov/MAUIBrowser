using MAUIBrowser.Abstractions;
using MAUIBrowser.Models;
using MAUIBrowser.Pages;
using MAUIBrowser.State;
using System.Windows.Input;

namespace MAUIBrowser.ViewModels
{
    public class TabsCollectionPageModel : BindableObject
    {

        #region Private property 
        private TabInfoModel selectedTab;
        private readonly ITabsPopupService popupService;
        #endregion

        #region Private property 
        public TabInfoModel SelectedTab
        {
            get => selectedTab; set
            {
                selectedTab = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public BrowserState BrowserState { get; }

        public TabsCollectionPageModel(ITabsPopupService popupService, BrowserState browserState)
        {
            this.popupService = popupService;
            BrowserState = browserState;
        }

        #region Commands 
        public ICommand CreateTabCommand => new Command(async () =>
        {
            if (Application.Current?.MainPage is not ContentPage contentPage)
                return;

            await popupService.CloseAsync();

            var tab = new TabInfoModel
            {
                Title = "Home",
                Url = "",
                Content = new HomePanelView()
            };

            BrowserState.CurrentTab = tab;
            BrowserState.Tabs.Add(tab);

            contentPage.Content = tab.Content;
        });

        public ICommand DeleteTabCommand => new Command<TabInfoModel>(async (tab) =>
        {
            if (tab == null)
                return;

            BrowserState.Tabs.Remove(tab);

            if (!BrowserState.Tabs.Any() && Application.Current?.MainPage is ContentPage contentPage)
            {
                BrowserState.CurrentTab = null;
                contentPage.Content = new HomePanelView();
                await popupService.CloseAsync();
            }
        });


        public ICommand TabSelectedCommand => new Command(async () =>
        {
            if (SelectedTab == null || Application.Current?.MainPage is not ContentPage contentPage)
                return;

            BrowserState.CurrentTab = SelectedTab;

            contentPage.Content = SelectedTab.Content;
            await popupService.CloseAsync();
        });

        #endregion
    }
}

