namespace MyWarsha_Interfaces.RepositoriesInterfaces
{
    public interface IRepository<T> where T : class
    {
        Task Add(T entity);
        Task AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> SaveChanges();
        int Count();
    }
}