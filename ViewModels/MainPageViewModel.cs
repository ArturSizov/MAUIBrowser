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
    #endregion


    #region Commands 
    public ICommand BackCommand => new DelegateCommand(async () =>
    {
        await Shell.Current.Navigation.PopAsync();
    });

    #endregion
}
