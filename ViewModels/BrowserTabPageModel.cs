using System.Windows.Input;

namespace MAUIBrowser.ViewModels
{
    public class BrowserTabPageModel : BindableObject
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

        #region Commands 
        public ICommand EnterAddressCommand => new Command(()=>
        {
            OnPropertyChanged(nameof(Url));
        });

      #endregion
    }
}
