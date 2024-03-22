using System.Linq.Expressions;

namespace Trendimaa.DAL.Interface
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T? Find(object id);
        T Create(T entity);
        void Update(T entity, T unchanged);
        Task<List<T>> CreateRangeAsync(List<T> entities);


        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter);
        Task<T?> FindAsync(object id);
        Task<T?> GetByFilterAsync(Expression<Func<T, bool>> filter, bool asNoTracking = false);
        Task<IQueryable<T>> GetQuery();
        void Remove(T entity);
        Task RemoveRange(List<T> entities);
        Task<T> CreateAsync(T entity);



    }
}
