using SQLite;

namespace MAUIBrowser.Abstractions
{
    public interface IDataProvider
    {
        public SQLiteAsyncConnection Database { get; set; }
        public Task Init(string databasePath);
    }
}
