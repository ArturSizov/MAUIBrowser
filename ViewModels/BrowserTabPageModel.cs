using MAUIBrowser.Pages;
using MAUIBrowser.State;
using Microsoft.Maui.Controls;
using System.Windows.Input;

namespace MAUIBrowser.ViewModels
{
    public class BrowserTabPageModel : BindableObject
    {
        #region Private property 
        private UrlWebViewSource url;
        private BrowserState state;
        #endregion

        #region Public property 
        public UrlWebViewSource Url
        {
            get => url; 
            set
            {
                url = value;
                OnPropertyChanged(nameof(Url));
             }
        }
        #endregion
        #region Commands    
        public ICommand AddressEntryCompleted => new Command<Microsoft.Maui.Controls.WebNavigatedEventArgs>((args) =>
        {
            if (args.Source is not UrlWebViewSource source)
                return;
            Url.Url = source.Url;
        });  
        #endregion
    }
}
