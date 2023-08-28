using MAUIBrowser.Abstractions;
using MAUIBrowser.Models;

namespace MAUIBrowser.DataAccessLayer.Implementations
{
    public class HistoryData : IHistoryData<HistoryModel>
    {
        #region Private property 
        private IDataProvider data;
        #endregion

        #region Public property 
        public string DatabasePath { get; set; }

        #endregion

        public HistoryData(IDataProvider data)
        {
            this.data = data;
        }

        #region Methods 
        /// <summary>
        /// Remove history db
        /// </summary>
        /// <param name="history"></param>
        /// <returns></returns>
        public async Task RemoveAsync(HistoryModel history)
        {
            if (DatabasePath is not null)
            {
                await data.InitAsync(DatabasePath);
                await data.Database.DeleteAsync(history);
            }
        }

        /// <summary>
        /// Get one historys 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<HistoryModel> GetAsync(int id)
        {
            if (DatabasePath is not null)
            {
                await data.InitAsync(DatabasePath);
                return await data.Database.Table<HistoryModel>().Where(i => i.Id == id).FirstOrDefaultAsync();
            }
            return new HistoryModel();
        }

        /// <summary>
        /// Get all histories 
        /// </summary>
        /// <returns></returns>
        public async Task<List<HistoryModel>> GetAllAsync()
        {
            if (DatabasePath is not null)
            {
                await data.InitAsync(DatabasePath);
                return await data.Database.Table<HistoryModel>().ToListAsync();
            }
            return new List<HistoryModel>();
        }

        /// <summary>
        /// Insert new history
        /// </summary>
        /// <param name="history"></param>
        /// <returns></returns>
        public async Task InsertAsync(HistoryModel history)
        {
            if (DatabasePath is not null)
            {
                await data.InitAsync(DatabasePath);
                await data.Database.InsertAsync(history);
            }
        }
        #endregion

    }
}
