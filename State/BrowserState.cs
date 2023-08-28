using MAUIBrowser.Abstractions;
using MAUIBrowser.Models;
using System.Collections.ObjectModel;

namespace MAUIBrowser.State
{
    public class BrowserState : BindableObject, IRepository<HistoryModel>
    {

        #region Private property 
        private IHistoryData<HistoryModel> data;

        #endregion

        #region Public property 
        public ObservableCollection<TabInfoModel> Tabs { get; } = new();
        public ObservableCollection<HistoryModel> Histories { get; set; } = new();

        public TabInfoModel? CurrentTab;
        #endregion

        public BrowserState(IHistoryData<HistoryModel> data)
        {
            this.data = data;
            data.DatabasePath = Path.Combine(FileSystem.AppDataDirectory, "history.db");
            Task.Run(async() => Histories = new ObservableCollection<HistoryModel>(await GetAllHistory())).Wait();
        }

        #region Methods

        // Get all history
        private async Task<IList<HistoryModel>> GetAllHistory()
        {
            return new ObservableCollection<HistoryModel>(await data.GetAllAsync()).ToList();
        }

        /// <summary>
        /// Insert new history
        /// </summary>
        /// <param name="history"></param>
        /// <returns></returns>
        public async Task Insert(HistoryModel history)
        {
            Histories.Add(history);
            await data.InsertAsync(history);
        }

        /// <summary>
        /// Remove one history
        /// </summary>
        /// <param name="history"></param>
        /// <returns></returns>
        public async Task Remove(HistoryModel history)
        {
            Histories.Remove(history);
            await data.RemoveAsync(history);
        }

        /// <summary>
        /// Remove all histories
        /// </summary>
        /// <returns></returns>
        public async Task RemoveAll()
        {
            foreach (var item in Histories)
            {
                await data.RemoveAsync(item);
            }
            Histories.Clear();
        }

        #endregion
    }
}
