namespace MAUIBrowser.Models
{
    /// <summary>
    /// Tab Info Model
    /// </summary>
    public class TabInfoModel : BindableObject
    {
        private string title = string.Empty;
        private string url = string.Empty;
        private ContentView content = new();

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
    }
}
