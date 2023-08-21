using InputKit.Shared.Controls;
using UraniumUI.Pages;

namespace MAUIBrowser.Pages;

public partial class MainPage : UraniumContentPage
{
	public MainPage()
    {
        SelectionView.GlobalSetting.CornerRadius = 0;
        InitializeComponent();
	}
}