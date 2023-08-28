using MAUIBrowser.Abstractions;
using MAUIBrowser.Auxiliary;
using MAUIBrowser.Models;
using Microsoft.Extensions.Logging;
using SQLite;

namespace MAUIBrowser.DataAccessLayer
{
    /// <summary>
    /// HistoryData provider
    /// </summary>
    public class HistoryDataSQLiteProvider : IHistoryDataProvider<HistoryModel>
    {
        private const SQLiteOpenFlags _flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache;

        private readonly ILogger _logger;
        private readonly string _connectionString;
        private SQLiteAsyncConnection? _database;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="options">Connection options</param>
        public HistoryDataSQLiteProvider(ILogger<HistoryDataSQLiteProvider> logger, DbConnectionOptions options)
        {
            _logger = logger;
            _connectionString = options.ConnectionString;
            Task.Run(InitAsync);
        }

        #region Methods `
        /// <inheritdoc/>
        public async Task CreateAsync(HistoryModel item)
        {
            if (_database is null)
                return;

            try
            {
                await _database.InsertAsync(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in CreateAsync()");
            }
        }

        /// <inheritdoc/>
        public async Task<HistoryModel?> ReadAsync(int id)
        {
            if (_database is null)
                return null;

            try
            {
                return await _database.Table<HistoryModel>().Where(i => i.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in ReadAsync()");
                return null;
            }
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(HistoryModel item)
        {
            if (_database is null)
                return;

            try
            {
                await _database.DeleteAsync(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in DeleteAsync()");
            }
        }

        /// <inheritdoc/>
        public async Task<List<HistoryModel>> ReadAllAsync()
        {
            if (_database is null)
                return new List<HistoryModel>();

            try
            {
                return await _database.Table<HistoryModel>().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in DeleteAsync()");
                return new List<HistoryModel>();
            }
        }

        /// <inheritdoc/>
        public async Task<int> DeleteAllAsync()
        {
            if (_database is null)
                return 0;

            try
            {
                return await _database.DeleteAllAsync<HistoryModel>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in DeleteAllAsync()");
                return 0;
            }
        }

        /// <inheritdoc/>
        public async Task<int> UpdateAsync(HistoryModel item)
        {
            if (_database is null)
                return 0;

            try
            {
                return await _database.UpdateAsync(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in UpdateAsync()");
                return 0;
            }
        }

        /// <summary>
        /// Initializes the database
        /// </summary>
        /// <returns></returns>
        private async Task InitAsync()
        {
            if (_database is not null)
                return;

            try
            {
                _database = new SQLiteAsyncConnection(_connectionString, _flags);
                await _database.CreateTableAsync<HistoryModel>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in InitAsync()");
            }
        }
        #endregion
    }
}

