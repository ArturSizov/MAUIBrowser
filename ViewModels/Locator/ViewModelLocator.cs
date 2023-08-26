using MAUIBrowser.Auxiliary;

namespace MAUIBrowser.ViewModels.Locator
{
    public class ViewModelLocator
    {
        public HomePanelViewModel HomePanelViewModel => RootContainer.Container.Resolve<HomePanelViewModel>();
        public BrowserTabPageModel BrowserTabPageModel => RootContainer.Container.Resolve<BrowserTabPageModel>();
        public BottomControlsViewModel BottomControlsViewModel => RootContainer.Container.Resolve<BottomControlsViewModel>();
        public TabsCollectionPopupModel TabsCollectionPopupModel => RootContainer.Container.Resolve<TabsCollectionPopupModel>();
        public HistoryPopupViewModel HistoryPopupViewModel => RootContainer.Container.Resolve<HistoryPopupViewModel>();

    }
}
