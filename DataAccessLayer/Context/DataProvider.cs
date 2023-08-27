using MAUIBrowser.Abstractions;
using MAUIBrowser.Models;
using SQLite;

namespace MAUIBrowser.DataAccessLayer.Context
{
    public class DataProvider : IDataProvider
    {
        private const SQLiteOpenFlags flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache;

        public SQLiteAsyncConnection Database { get; set; }

        /// <summary>
        /// Database initialization
        /// </summary>
        /// <param name="databasePath"></param>
        /// <returns></returns>
        public async Task Init(string databasePath)
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(databasePath, flags);
            await Database.CreateTableAsync<HistoryModel>();
        }
    }
}

