using MAUIBrowser.Abstractions;
using MAUIBrowser.Auxiliary;
using MAUIBrowser.DataAccessLayer.DAO;
using Microsoft.Extensions.Logging;
using SQLite;

namespace MAUIBrowser.DataAccessLayer
{
    /// <summary>
    /// HistoryData provider
    /// </summary>
    public class HistoryDataSQLiteProvider : IDataProvider<HistoryInfoDAO>
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

		/// <inheritdoc/>
		public Task<int> CreateAsync(HistoryInfoDAO item)
        {
            if (_database is null)
				return Task.FromResult(0);

			try
            {
                return _database.InsertAsync(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in CreateAsync()");
				return Task.FromResult(0);
			}
        }

        /// <inheritdoc/>
        public Task<HistoryInfoDAO?> ReadAsync(int id)
        {
            if (_database is null)
                return Task.FromResult<HistoryInfoDAO?>(null);

            try
            {
                return _database.Table<HistoryInfoDAO?>().Where(x => x != null && x.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in ReadAsync()");
				return Task.FromResult<HistoryInfoDAO?>(null);
			}
        }

        /// <inheritdoc/>
        public Task<int> DeleteAsync(HistoryInfoDAO item)
        {
            if (_database is null)
                return Task.FromResult(0);

            try
            {
                return _database.DeleteAsync(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in DeleteAsync()");
				return Task.FromResult(0);
			}
        }

        /// <inheritdoc/>
        public Task<List<HistoryInfoDAO>> ReadAllAsync()
        {
            if (_database is null)
                return Task.FromResult(new List<HistoryInfoDAO>());

            try
            {
                return _database.Table<HistoryInfoDAO>().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in ReadAllAsync()");
				return Task.FromResult(new List<HistoryInfoDAO>());
			}
        }

        /// <inheritdoc/>
        public Task<int> DeleteAllAsync()
        {
            if (_database is null)
				return Task.FromResult(0);

			try
            {
                return _database.DeleteAllAsync<HistoryInfoDAO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in DeleteAllAsync()");
				return Task.FromResult(0);
			}
        }

        /// <inheritdoc/>
        public Task<int> UpdateAsync(HistoryInfoDAO item)
        {
            if (_database is null)
				return Task.FromResult(0);

			try
            {
                return _database.UpdateAsync(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in UpdateAsync()");
				return Task.FromResult(0);
			}
        }

		/// <summary>
		/// Initializes the database
		/// </summary>
		/// <returns></returns>
		private async Task InitAsync()
		{
			try
			{
				_database ??= new SQLiteAsyncConnection(_connectionString, _flags);
				var exists = await IsTableExists(nameof(HistoryInfoDAO));

				if (!exists)
					_ = await _database.CreateTableAsync<HistoryInfoDAO>();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Exception in InitAsync()");
			}
		}

		/// <summary>
		/// Cheks if table already exists
		/// </summary>
		/// <param name="tableName">Table name to check</param>
		/// <returns><see langword="true"/> if exists; otherwise <see langword="false"/></returns>
		private async Task<bool> IsTableExists(string tableName)
		{
			try
			{
				_database ??= new SQLiteAsyncConnection(_connectionString, _flags);
				var info = await _database.GetTableInfoAsync(tableName);
				return info != null && info.Count > 0;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Exception in IsTableExists()");
				return false;
			}
		}
	}
}

