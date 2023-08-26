using MAUIBrowser.Abstractions;

namespace MAUIBrowser.Pages;

public partial class BrowserTabPage : ContentView
{
	public BrowserTabPage(IWebViewServices web)
	{
		InitializeComponent();

		web.WebView = wv; //for navigation
	}
}