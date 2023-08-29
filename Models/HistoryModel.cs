namespace MAUIBrowser.Models
{
    public class HistoryModel : BindableObject
	{
		private int _id;
		private string _title = string.Empty;
		private DateTime _date = DateTime.UtcNow;
		private string _url = string.Empty;

		public int Id
		{
			get => _id;
			set
			{
				_id = value;
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
		public DateTime Date
		{
			get => _date;
			set
			{
				_date = value;
				OnPropertyChanged();
			}
		}

		public string Url
		{
			get => _url;
			set
			{
				_url = value;
				OnPropertyChanged();
			}
		}
    }
}
