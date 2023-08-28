namespace MAUIBrowser.Abstractions
{
    /// <summary>
    /// Implementing database initialization
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataProviderBase<T>
    {
        /// <summary>
        /// Adds one entry to the database
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task CreateAsync(T item);

        /// <summary>
        /// Returns one record from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T?> ReadAsync(int id);

        /// <summary>
        /// Updates one record in the database
        /// </summary>
        /// <param name="item">Item to update</param>
        /// <returns>The updated items count</returns>
        Task<int> UpdateAsync(T item);

        /// <summary>
        /// Deleting a Single Record in a Database
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task DeleteAsync(T item);

        /// <summary>
        /// Returns all records from the database
        /// </summary>
        /// <returns></returns>
        Task<List<T>> ReadAllAsync();

        /// <summary>
        /// Deletes all records from the database. Be careful!
        /// </summary>
        /// <returns>The deleted items count</returns>
        Task<int> DeleteAllAsync();
    }
}
