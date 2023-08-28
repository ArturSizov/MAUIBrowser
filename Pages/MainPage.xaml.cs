﻿using MAUIBrowser.Abstractions;
using UraniumUI.Pages;

namespace MAUIBrowser.Pages;

public partial class MainPage : UraniumContentPage
{
	private IWebViewServices<WebView> web;

	public MainPage(IWebViewServices<WebView> web)
	{
		this.web = web;
		InitializeComponent();
	}
	protected override bool OnBackButtonPressed()
	{
		return web.GoBack();
	}

}
