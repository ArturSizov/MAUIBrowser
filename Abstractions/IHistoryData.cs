namespace MAUIBrowser.Abstractions
{
    public interface IHistoryData<T>
    {
        string DatabasePath { get; set; }
        Task Remove(T t);
        Task<T> Get(int id);
        Task<List<T>> GetAll();
        Task Insert(T t);
    }
}
