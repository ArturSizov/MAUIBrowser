using MAUIBrowser.Abstractions;
using MAUIBrowser.Auxiliary;
using MAUIBrowser.DataAccessLayer.DAO;
using MAUIBrowser.Models;
using System.Collections.ObjectModel;

namespace MAUIBrowser.Managers
{
	/// <summary>
	/// FastLinks manager
	/// </summary>
	public class FastLinksManager : IBrowserStateManager<FastLinkModel>
	{
		/// <summary>
		/// Data provider
		/// </summary>
		private readonly IDataProvider<FastLinkInfoDAO> _dataProvider;

		/// <inheritdoc/>
		public ObservableCollection<FastLinkModel> Items { get; set; } = new();

		/// <summary>
		/// Ctor
		/// </summary>
		/// <param name="dataProvider">Data provider</param>
		public FastLinksManager(IDataProvider<FastLinkInfoDAO> dataProvider)
		{
			_dataProvider = dataProvider;
			Task.Run(ReadAllFastLinksAsync);
		}

		/// <inheritdoc/>
		public Task<int> CreateAsync(FastLinkModel item)
		{
			Items.Add(item);
			return _dataProvider.CreateAsync(item.ToDAO());
		}

		/// <inheritdoc/>
		public Task<FastLinkModel?> ReadAsync(int id)
		{
			var item = Items.FirstOrDefault(x => x.Id == id);
			return Task.FromResult(item);
		}

		/// <inheritdoc/>
		public Task<int> UpdateAsync(FastLinkModel item)
		{
			var foundItem = Items.FirstOrDefault(x => x.Id == item.Id);

			if (foundItem == null)
				return Task.FromResult(0);

			foundItem = item;
			return _dataProvider.UpdateAsync(item.ToDAO());
		}

		/// <inheritdoc/>
		public Task<int> DeleteAsync(FastLinkModel item)
		{
			var foundItem = Items.FirstOrDefault(x => x.Id == item.Id);

			if (foundItem == null)
				return Task.FromResult(0);

			Items.Remove(foundItem);
			return _dataProvider.DeleteAsync(item.ToDAO());
		}

		/// <inheritdoc/>
		public Task<List<FastLinkModel>> ReadAllAsync()
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
		private async Task ReadAllFastLinksAsync()
		{
			var items = await _dataProvider.ReadAllAsync();
			Items = new ObservableCollection<FastLinkModel>(items.Select(x => x.ToModel()));
		}
	}
}
