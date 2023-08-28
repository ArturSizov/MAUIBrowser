using MAUIBrowser.Abstractions;

namespace MAUIBrowser.Pages;

public partial class BrowserTabPage : ContentView
{
	public BrowserTabPage(IWebViewService<WebView> web)
	{
		InitializeComponent();

		web.WebView = wv; //for navigation
	}
}