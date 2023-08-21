using MAUIBrowser.Auxiliary;

namespace MAUIBrowser.ViewModels.Locator
{
    public class ViewModelLocator
    {
        public MainPageViewModel MainPageViewModel => RootContainer.Container.Resolve<MainPageViewModel>();
        public HomePanelViewModel HomePanelViewModel => RootContainer.Container.Resolve<HomePanelViewModel>();
        public BrowserTabPageModel BrowserTabPageModel => RootContainer.Container.Resolve<BrowserTabPageModel>();
        public BottomControlsViewModel BottomControlsViewModel => RootContainer.Container.Resolve<BottomControlsViewModel>();
        public TabsCollectionPageModel TabsCollectionPageModel => RootContainer.Container.Resolve<TabsCollectionPageModel>();
    }
}
