using Mopups.Hosting;
using UraniumUI;
using MAUIBrowser.ViewModels;
using MAUIBrowser.Auxiliary;
using MAUIBrowser.Abstractions;
using MAUIBrowser.Services;
using MAUIBrowser.State;
using CommunityToolkit.Maui;
using MAUIBrowser.Pages;
using MAUIBrowser.Models;
using MAUIBrowser.DataAccessLayer;
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
            .AddSingleton(new DbConnectionOptions { ConnectionString = Path.Combine(FileSystem.AppDataDirectory, "history.db") })
            .AddSingleton<IHistoryDataProvider<HistoryModel>, HistoryDataSQLiteProvider>()
            .AddSingleton<ITabsPopupService, TabsPopupService>()
            .AddSingleton<IWebViewService<WebView>, WebViewServices>()
            .AddSingleton<IHistoryPopupService, HistoryPopupServices>()
            .AddSingleton<BrowserState>()
            .AddSingleton<ISettingsService, SettingsService>()


            // register Pages
            .AddTransient<MainPage>()
			.AddTransient<BrowserTabPage>()

			// register ViewModels
			.AddSingleton<BrowserTabPageModel>()
			.AddTransient<HomePanelViewModel>()
			.AddSingleton<BottomControlsViewModel>()
			.AddSingleton<TabsCollectionPopupModel>()
			.AddSingleton<HistoryPopupViewModel>();
		

		var app = builder.Build();

        RootContainer.Container.Initialize(app.Services);

        return app; 
	}
}
