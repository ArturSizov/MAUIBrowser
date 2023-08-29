using SQLite;

namespace MAUIBrowser.Models
{
    /// <summary>
    /// Fast Link Model
    /// </summary>
    public class FastLinkModel : BindableObject
	{
		private int _id;
		private string _title = string.Empty;
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

		public string Url
		{
			get => _url;
			set
			{
				_url = value;
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
    }
}
