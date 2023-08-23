using Mopups.Hosting;
using UraniumUI;
using MAUIBrowser.ViewModels;
using MAUIBrowser.Auxiliary;
using MAUIBrowser.Abstractions;
using MAUIBrowser.Services;
using MAUIBrowser.State;
using CommunityToolkit.Maui;
using MAUIBrowser.Pages;

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

            });

		builder.Services.AddMopupsDialogs()

            // register Services
            .AddSingleton<ITabsPopupService, TabsPopupService>()
            .AddSingleton<BrowserState>()

            // register Pages
            .AddTransient<MainPage>()
            .AddTransient<BrowserTabPage>()

            // register ViewModels
            .AddSingleton<MainPageViewModel>()
			.AddSingleton<BrowserTabPageModel>()
			.AddTransient<HomePanelViewModel>()
			.AddSingleton<BottomControlsViewModel>()
			.AddSingleton<TabsCollectionPageModel>();
		

		var app = builder.Build();

        RootContainer.Container.Initialize(app.Services);

        return app; 
	}
}
