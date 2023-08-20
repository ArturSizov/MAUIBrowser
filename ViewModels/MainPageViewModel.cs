using System.Collections.ObjectModel;
using System.Windows.Input;
using UraniumUI;
using Prism.Mvvm;
using Prism.Commands;

namespace MAUIBrowser.ViewModels;
public class MainPageViewModel : BindableBase
{

    #region Private property 
    private bool isVisible = true;

    #endregion

    #region Public property 
    public string Title => "MAUI Browser";
    public string Url { get; set; } = "https://www.google.com/";
    public bool IsVisible { get => isVisible; set => SetProperty(ref isVisible, value); } 
    #endregion


    #region Commands 
    public ICommand RefreshWebSours => new DelegateCommand(async() =>
    {
        RaisePropertyChanged(nameof(Url));

        IsVisible = false;       
    });

    #endregion
}
