using MAUIBrowser.Auxiliary;
using MAUIBrowser.State;
using System.Windows.Input;

namespace MAUIBrowser.ViewModels
{
    public class BrowserTabPageModel : BindableObject
    {
        #region Private property 
        private string url = string.Empty;
        private BrowserState state;
        #endregion

        #region Public property 
        public string Url { get; set; } = string.Empty;
        public string EntryUrl { get; set; } = string.Empty;

        public BrowserTabPageModel()
        {
            EntryUrl = Url;
            OnPropertyChanged(nameof(EntryUrl));
        }
        #endregion
        #region Commands 
        public ICommand EnterAddressCommand => new Command(() =>
        {
            var url = WebViewSourceBuilder.Create(EntryUrl);
            Url = url;
            EntryUrl = url;
            OnPropertyChanged(nameof(EntryUrl));
            OnPropertyChanged(nameof(Url));
        });
        public ICommand AddressEntryCompleted => new Command<Microsoft.Maui.Controls.WebNavigatedEventArgs>((args) =>
        {
            if (args.Source is not UrlWebViewSource source)
                return;
            
            Url = source.Url;
            EntryUrl = source.Url;
            OnPropertyChanged(nameof(EntryUrl));
        });  
        #endregion
    }
}
