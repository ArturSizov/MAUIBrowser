using MAUIBrowser.Abstractions;

namespace MAUIBrowser;

public partial class App : Application
{
	public App(IWebViewService<WebView> web)
	{
		InitializeComponent();

		MainPage = new Pages.MainPage(web); // AppShell();                                        
    }
}
