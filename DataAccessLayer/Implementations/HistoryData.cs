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
        public async Task Remove(HistoryModel history)
        {
            if (DatabasePath is not null)
            {
                await data.Init(DatabasePath);
                await data.Database.DeleteAsync(history);
            }
        }

        /// <summary>
        /// Get one historyS 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<HistoryModel> Get(int id)
        {
            if (DatabasePath is not null)
            {
                await data.Init(DatabasePath);
                return await data.Database.Table<HistoryModel>().Where(i => i.Id == id).FirstOrDefaultAsync();
            }
            return new HistoryModel();
        }

        /// <summary>
        /// Get all histories 
        /// </summary>
        /// <returns></returns>
        public async Task<List<HistoryModel>> GetAll()
        {
            if (DatabasePath is not null)
            {
                await data.Init(DatabasePath);
                return await data.Database.Table<HistoryModel>().ToListAsync();
            }
            return new List<HistoryModel>();
        }

        /// <summary>
        /// Insert new history
        /// </summary>
        /// <param name="history"></param>
        /// <returns></returns>
        public async Task Insert(HistoryModel history)
        {
            if (DatabasePath is not null)
            {
                await data.Init(DatabasePath);
                await data.Database.InsertAsync(history);
            }
        }
        #endregion

    }
}
