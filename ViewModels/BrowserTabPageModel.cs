using MAUIBrowser.Auxiliary;

namespace MAUIBrowser.ViewModels
{
    public class BrowserTabPageModel : BindableObject
    {
        #region Private property 
        private string url = string.Empty;

        #endregion
        #region Public property 
        public string Title => "MAUI Browser";
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
        #region Events 
        private void AddressEntryCompleted(object sender, EventArgs e)
        {
            var url = WebViewSourceBuilder.Create(Url);
            Url = url;
        }
        #endregion
    }
}
