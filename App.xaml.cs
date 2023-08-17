using UraniumUI.Material.Resources;

namespace MAUIBrowser;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}
