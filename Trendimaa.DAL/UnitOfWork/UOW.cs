using Trendimaa.DAL.Abstract;
using Trendimaa.DAL.Context;
using Trendimaa.DAL.Interface;

namespace Trendimaa.DAL.UnitOfWork
{
    public class UOW : IUOW
    {
        private readonly TrendimaaContext _context;

        public UOW(TrendimaaContext context)
        {
            _context = context;
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return new Repository<T>(_context);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
