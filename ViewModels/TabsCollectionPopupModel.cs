using MAUIBrowser.Abstractions;
using MAUIBrowser.Models;
using MAUIBrowser.Pages;
using MAUIBrowser.State;
using System.Windows.Input;

namespace MAUIBrowser.ViewModels
{
    public class TabsCollectionPopupModel : BindableObject
    {

        #region Private property 
        private TabInfoModel selectedTab;
        private readonly ITabsPopupService popupService;
        #endregion

        #region Public property 
        public BrowserState BrowserState { get; }

        public TabInfoModel SelectedTab
        {
            get => selectedTab; 
            set
            {
                selectedTab = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public TabsCollectionPopupModel(ITabsPopupService popupService, BrowserState browserState)
        {
            this.popupService = popupService;
            BrowserState = browserState;
        }

        #region Commands
        /// <summary>
        /// Blank page creation command
        /// </summary>
        public ICommand CreateTabCommand => new Command(async() =>
        {
            if (Application.Current?.MainPage is not ContentPage contentPage)
                return;

            await popupService.CloseAsync();

            var tab = new TabInfoModel
            {
                Title = "Home",
                Url = string.Empty,
                Content = new HomePanelView()
            };

            BrowserState.CurrentTab = tab;
            BrowserState.Tabs.Add(tab);

            contentPage.Content = tab.Content;
        });

        /// <summary>
        /// Single page delete command
        /// </summary>
        public ICommand DeleteTabCommand => new Command<TabInfoModel>(async(tab) =>
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

        /// <summary>
        /// Command to add a new page
        /// </summary>
        public ICommand TabSelectedCommand => new Command(async() =>
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

