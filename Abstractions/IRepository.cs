namespace MAUIBrowser.Abstractions
{
    public interface IRepository<T>
    {
        Task Insert(T t);
        Task Remove(T t);
        Task RemoveAll();
    }
}
