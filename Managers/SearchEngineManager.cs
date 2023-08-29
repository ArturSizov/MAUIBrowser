using MAUIBrowser.Abstractions;
using MAUIBrowser.Models;
using System.Collections.ObjectModel;

namespace MAUIBrowser.Managers
{
	/// <summary>
	/// SearchEngin manager
	/// </summary>
	public class SearchEngineManager : IBrowserStateManager<SearchEngineModel>
	{
		/// <inheritdoc/>
		public ObservableCollection<SearchEngineModel> Items { get; set; } = new()
		{
			new()
			{
				Image = "google.png",
				SearchQuery = "https://www.google.com/search?q="
			},
			new()
			{
				Image = "rambler.png",
				SearchQuery = "https://nova.rambler.ru/search?query="
			},
			new()
			{
				Image = "yandex.png",
				SearchQuery = "https://ya.ru/search/?text="
			}
		};

		/// <inheritdoc/>
		public Task<int> CreateAsync(SearchEngineModel item) 
			=> throw new NotSupportedException();

		/// <inheritdoc/>
		public Task<SearchEngineModel?> ReadAsync(int id)
			=> throw new NotSupportedException();

		/// <inheritdoc/>
		public Task<int> UpdateAsync(SearchEngineModel item)
			=> throw new NotSupportedException();

		/// <inheritdoc/>
		public Task<int> DeleteAsync(SearchEngineModel item)
			=> throw new NotSupportedException();

		/// <inheritdoc/>
		public Task<List<SearchEngineModel>> ReadAllAsync()
			=> throw new NotSupportedException();

		/// <inheritdoc/>
		public Task<int> DeleteAllAsync()
			=> throw new NotSupportedException();
	}
}
