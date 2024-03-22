using Trendimaa.DAL.Interface;

namespace Trendimaa.DAL.UnitOfWork
{

    public interface IUOW
    {
        IRepository<T> GetRepository<T>() where T : class;
        Task SaveChangesAsync();
        void SaveChanges();
    }
}
