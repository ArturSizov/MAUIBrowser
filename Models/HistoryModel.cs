namespace MAUIBrowser.Models
{
    public class HistoryModel : BindableObject
    {
        #region Private property 
        private DateTime date; private string title = string.Empty;
        private string url = string.Empty;

        #endregion

        #region Public property 
        public DateTime Date
        {
            get => date;
            set
            {
                date = value;
                OnPropertyChanged();
            }
        }
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

        #endregion
    }
}
