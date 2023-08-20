using MAUIBrowser.Auxiliary;

namespace MAUIBrowser.ViewModels.Locator
{
    public class ViewModelLocator
    {
        public MainPageViewModel MainPageViewModel => RootContainer.Container.Resolve<MainPageViewModel>();
    }
}
