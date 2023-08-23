namespace MAUIBrowser.Models
{
    public class TabInfoModel : BindableObject
    {
        #region Private property 
        private string title = string.Empty;
        private string url = string.Empty;
        private ContentView content = new();
        #endregion

        #region Public property 
        public string Title
        {
            get => title; 
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }
        public string Url
        {
            get => url; 
            set
            {
                url = value;
                OnPropertyChanged();
            }
        }
        public ContentView Content
        {
            get => content; 
            set
            {
                content = value;
                OnPropertyChanged();
            }
        }
        #endregion
    }
}
