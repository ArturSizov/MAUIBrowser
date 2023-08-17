using System.Collections.ObjectModel;
using System.Windows.Input;
using UraniumUI;
using Prism.Mvvm;
using Prism.Commands;

namespace MAUIBrowser.ViewModels;
public class MainPageViewModel : BindableBase
{

	#region Public property 
	public string Title => "MAUI Browser";
    public string WebSours { get; set; } = "https://www.google.com/";
    #endregion


    #region Commands 
    public ICommand RefreshWebSours => new DelegateCommand(async () =>
    {
        RaisePropertyChanged(nameof(WebSours));
        
    });

    #endregion
}
