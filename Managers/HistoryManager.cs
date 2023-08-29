using MAUIBrowser.Abstractions;
using MAUIBrowser.Auxiliary;
using MAUIBrowser.DataAccessLayer.DAO;
using MAUIBrowser.Models;
using System.Collections.ObjectModel;

namespace MAUIBrowser.Managers
{
	/// <summary>
	/// History data manager
	/// </summary>
	public class HistoryManager : IBrowserStateManager<HistoryModel>
	{
		/// <summary>
		/// History data provider
		/// </summary>
		private readonly IDataProvider<HistoryInfoDAO> _dataProvider;

		/// <inheritdoc/>
		public ObservableCollection<HistoryModel> Items { get; set; } = new();

		/// <summary>
		/// Ctor
		/// </summary>
		/// <param name="dataProvider">History data provider</param>
		public HistoryManager(IDataProvider<HistoryInfoDAO> dataProvider)
		{
			_dataProvider = dataProvider;
			Task.Run(ReadAllHistoryAsync);
		}

		/// <inheritdoc/>
		public Task<int> CreateAsync(HistoryModel item)
		{
			Items.Add(item);
			return _dataProvider.CreateAsync(item.ToDAO());
		}

		/// <inheritdoc/>
		public Task<HistoryModel?> ReadAsync(int id)
		{
			var item = Items.FirstOrDefault(x => x.Id == id);
			return Task.FromResult(item);
		}

		/// <inheritdoc/>
		public Task<int> UpdateAsync(HistoryModel item)
		{
			var foundItem = Items.FirstOrDefault(x => x.Id == item.Id);

			if (foundItem == null)
				return Task.FromResult(0);

			foundItem = item;
			return _dataProvider.UpdateAsync(item.ToDAO());
		}

		/// <inheritdoc/>
		public Task<int> DeleteAsync(HistoryModel item)
		{
			var foundItem = Items.FirstOrDefault(x => x.Id == item.Id);

			if (foundItem == null)
				return Task.FromResult(0);

			Items.Remove(foundItem);
			return _dataProvider.DeleteAsync(item.ToDAO());
		}

		/// <inheritdoc/>
		public Task<List<HistoryModel>> ReadAllAsync()
			=> Task.FromResult(Items.ToList());
		

		/// <inheritdoc/>
		public Task<int> DeleteAllAsync()
		{
			Items.Clear();
			return _dataProvider.DeleteAllAsync();
		}

		/// <summary>
		/// Reads all data from db
		/// </summary>
		private async Task ReadAllHistoryAsync()
		{
			var items = await _dataProvider.ReadAllAsync();
			Items = new ObservableCollection<HistoryModel>(items.Select(x => x.ToModel()));
		}
	}
}
