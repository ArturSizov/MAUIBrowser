namespace MAUIBrowser.Abstractions
{
    /// <summary>
    /// Implementation of writing to the database
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Adds one entry
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task Insert(T t);

        /// <summary>
        /// Deletes one entry
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task Remove(T t);

        /// <summary>
        /// Deletes all entries
        /// </summary>
        /// <returns></returns>
        Task RemoveAll();
    }
}
