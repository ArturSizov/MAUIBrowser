using Mopups.Hosting;
using InputKit.Shared.Controls;
using UraniumUI;
using MAUIBrowser.ViewModels;
using MAUIBrowser.Auxiliary;

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
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFontAwesomeIconFonts();

            });

		builder.Services.AddMopupsDialogs().
						 AddSingleton<MainPageViewModel>();

		var app = builder.Build();

        RootContainer.Container.Initialize(app.Services);

        return app; 
	}
}
