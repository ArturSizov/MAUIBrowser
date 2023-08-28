namespace MAUIBrowser.Abstractions
{
    /// <summary>
    /// Implementing a Database for Browsing History
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IHistoryData<T>
    {
        string DatabasePath { get; set; }

        /// <summary>
        /// Deleting a Single Record in a Database
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task RemoveAsync(T t);

        /// <summary>
        /// Returns one record from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetAsync(int id);

        /// <summary>
        /// Returns all records from the database
        /// </summary>
        /// <returns></returns>
        Task<List<T>> GetAllAsync();

        /// <summary>
        /// Adds one entry to the database
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task InsertAsync(T t);
    }
}
