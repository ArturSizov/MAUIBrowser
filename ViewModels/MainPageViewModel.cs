namespace MAUIBrowser.ViewModels;
public class MainPageViewModel : BindableObject
{
    #region Private property 
    private string url = string.Empty;
    #endregion

    #region Public property
    public string Url
    {
        get => url; 
        set
        {
            url = value;
            OnPropertyChanged();
        }
    }

    #endregion
}
