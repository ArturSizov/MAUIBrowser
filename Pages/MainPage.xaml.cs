using MAUIBrowser.Abstractions;
using UraniumUI.Pages;

namespace MAUIBrowser.Pages;

public partial class MainPage : UraniumContentPage
{
	private IWebViewService<WebView> web;

	public MainPage(IWebViewService<WebView> web)
	{
		this.web = web;
		InitializeComponent();
	}
	protected override bool OnBackButtonPressed()
	{
		return web.GoBack();
	}

}
