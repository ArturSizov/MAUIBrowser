using Mopups.Hosting;
using UraniumUI;
using MAUIBrowser.ViewModels;
using MAUIBrowser.Auxiliary;
using MAUIBrowser.Abstractions;
using MAUIBrowser.Services;
using MAUIBrowser.State;
using CommunityToolkit.Maui;
using MAUIBrowser.Pages;
#if ANDROID
	using MAUIBrowser.Platforms.Android.Handlers;
#endif

namespace MAUIBrowser;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureMopups()
			.UseUraniumUI()
			.UseUraniumUIMaterial()
            .UseUraniumUIBlurs()
			.UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFontAwesomeIconFonts();
				fonts.AddMaterialIconFonts();

            })
            .ConfigureMauiHandlers(handlers =>
            {
#if ANDROID
                handlers.AddHandler(typeof(Entry), typeof(CustomEntryHandler)); // removes the underline
#endif
            });

		builder.Services.AddMopupsDialogs()

			// register Services
			.AddSingleton<ITabsPopupService, TabsPopupService>()
			.AddSingleton<IWebViewServices, WebViewServices>()
			.AddSingleton<ISettingsPopupServices, SettingsPopupServices>()
            .AddSingleton<BrowserState>()

			// register Pages
			.AddTransient<MainPage>()
			.AddTransient<BrowserTabPage>()

			// register ViewModels
			.AddSingleton<BrowserTabPageModel>()
			.AddTransient<HomePanelViewModel>()
			.AddSingleton<BottomControlsViewModel>()
			.AddSingleton<TabsCollectionPageModel>()
			.AddSingleton<SettingsViewModel>();
		

		var app = builder.Build();

        RootContainer.Container.Initialize(app.Services);

        return app; 
	}
}
