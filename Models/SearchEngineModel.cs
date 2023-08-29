namespace MAUIBrowser.Models
{
    public class SearchEngineModel : BindableObject
	{
		private string _image = string.Empty;
		private string _searchQuery = string.Empty;

		public string Image
		{
			get => _image;
			set
			{
				_image = value;
				OnPropertyChanged();
			}
		}

		public string SearchQuery
		{
			get => _searchQuery;
			set
			{
				_searchQuery = value;
				OnPropertyChanged();
			}
		}
    }
}
