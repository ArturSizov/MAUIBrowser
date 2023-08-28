namespace MAUIBrowser.Models
{
    public class FastLinkModel : BindableObject
    {
        #region Private property 
        private string _url = string.Empty;
        private string _title = string.Empty;
        #endregion

        #region Public poperty 
        public string Url
        {
            get => _url;
            set
            {
                _url = value;
                OnPropertyChanged();
            }
        }
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }
        public string IconSource
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Url) || !Uri.TryCreate(Url, UriKind.Absolute, out var uri) || uri == null)
                    return "uranium.png";

                return $"{uri.Scheme}://{uri.Host}/favicon.ico";
            }
        }
        #endregion
    }
}
