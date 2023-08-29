using MAUIBrowser.Abstractions;
using MAUIBrowser.Models;
using System.Collections.ObjectModel;

namespace MAUIBrowser.State
{
    public class HistoryState
    {
        #region Private propery 
        private IHistoryDataProvider<HistoryModel> historyDataProvider;
        #endregion
        #region Public property 
        public ObservableCollection<HistoryModel> Histories { get; set; } = new();
        #endregion

        public HistoryState(IHistoryDataProvider<HistoryModel> historyDataProvider)
        {
            this.historyDataProvider = historyDataProvider;
            Task.Run(async () => Histories = new ObservableCollection<HistoryModel>(await GetAllHistoryAsync())).Wait();
        }

        #region Methods

        // Get all history
        private async Task<IList<HistoryModel>> GetAllHistoryAsync()
        {
            return new ObservableCollection<HistoryModel>(await historyDataProvider.ReadAllAsync()).ToList();
        }

        /// <summary>
        /// Insert new history
        /// </summary>
        /// <param name="history"></param>
        /// <returns></returns>
        public async Task InsertAsync(HistoryModel history)
        {
            Histories.Add(history);
            await historyDataProvider.CreateAsync(history);
        }

        /// <summary>
        /// Remove one history
        /// </summary>
        /// <param name="history"></param>
        /// <returns></returns>
        public async Task RemoveAsync(HistoryModel history)
        {
            Histories.Remove(history);
            await historyDataProvider.DeleteAsync(history);
        }

        /// <summary>
        /// Remove all histories
        /// </summary>
        /// <returns></returns>
        public async Task RemoveAllAsync()
        {
            await historyDataProvider.DeleteAllAsync();
            Histories.Clear();
        }

        #endregion
    }
}
