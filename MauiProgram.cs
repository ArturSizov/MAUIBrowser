using Mopups.Hosting;
using UraniumUI;
using MAUIBrowser.ViewModels;
using MAUIBrowser.Auxiliary;
using MAUIBrowser.Abstractions;
using MAUIBrowser.Services;
using MAUIBrowser.State;
using CommunityToolkit.Maui;
using MAUIBrowser.Models;
using MAUIBrowser.DataAccessLayer;
using MAUIBrowser.DataAccessLayer.DAO;
using Microsoft.Extensions.Logging;
using MAUIBrowser.Managers;
#if ANDROID
	using MAUIBrowser.Platforms.Android.Handlers;
#endif

namespace MAUIBrowser;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder.Logging.AddDebug();
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
			.AddSingleton<IDataProvider<HistoryInfoDAO>, HistoryDataSQLiteProvider>()
			.AddSingleton<IDataProvider<FastLinkInfoDAO>, FastLinksSQLiteProvider>()
			.AddSingleton<ITabsPopupService, TabsPopupService>()
			.AddSingleton<IWebViewService<WebView>, WebViewServices>()
			.AddSingleton<IHistoryPopupService, HistoryPopupServices>()
			.AddSingleton<BrowserState>()

			.AddSingleton<IBrowserStateManager<FastLinkModel>, FastLinksManager>()
			.AddSingleton<IBrowserStateManager<HistoryModel>, HistoryManager>()
			.AddSingleton<IBrowserStateManager<SearchEngineModel>, SearchEngineManager>()

            .AddSingleton<ISettingsService, SettingsService>()

			// register ViewModels
			.AddSingleton<BrowserTabPageModel>()
			.AddTransient<HomePanelViewModel>()
			.AddSingleton<BottomControlsViewModel>()
			.AddSingleton<TabsCollectionPopupModel>()
			.AddSingleton<HistoryPopupViewModel>();
		

		var app = builder.Build();

		// a workaround to initialize the SQL providers before pages
		_ = app.Services.GetRequiredService<IDataProvider<HistoryInfoDAO>>();
		_ = app.Services.GetRequiredService<IDataProvider<FastLinkInfoDAO>>();

		RootContainer.Container.Initialize(app.Services);

        return app; 
	}
}
