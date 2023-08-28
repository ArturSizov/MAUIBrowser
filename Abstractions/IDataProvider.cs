namespace MAUIBrowser.Abstractions
{
    /// <summary>
    /// Implementing database initialization
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataProvider<T>
    {
        public T Database { get; set; }

        /// <summary>
        /// Initializes the database
        /// </summary>
        /// <param name="databasePath"></param>
        /// <returns></returns>
        public Task InitAsync(string databasePath);
    }
}
