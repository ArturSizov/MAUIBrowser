namespace MAUIBrowser;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new MAUIBrowser.Pages.MainPage();
		//MainPage = new AppShell();
	}
}
