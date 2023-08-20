using System.Collections.ObjectModel;
using System.Windows.Input;
using UraniumUI;
using Prism.Mvvm;
using Prism.Commands;

namespace MAUIBrowser.ViewModels;
public class MainPageViewModel : BindableBase
{
    #region Private property 
    private string url = string.Empty;
    #endregion

    #region Public property
    public string Url { get => url; set => SetProperty(ref url, value); }

    #endregion
}
