using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Trendimaa.DAL.Context;
using Trendimaa.DAL.Interface;

namespace Trendimaa.DAL.Abstract
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly TrendimaaContext _context;

        public Repository(TrendimaaContext context)
        {
            _context = context;
        }


        public List<T> GetAll()
        {

            return _context.Set<T>().AsNoTracking().ToList();

        }

        public T? Find(object id)
        {
            return _context.Set<T>().Find(id);

        }

        public T Create(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;

        }
        public void Update(T entity, T unchanged)
        {
            _context.Entry(unchanged).CurrentValues.SetValues(entity);
            _context.SaveChanges();
        }





        //ASYNC METODLAR

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter)
        {
            return await _context.Set<T>().Where(filter).ToListAsync();
        }



        public async Task<T?> FindAsync(object id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T?> GetByFilterAsync(Expression<Func<T, bool>> filter, bool asNoTracking = false)
        {
            return !asNoTracking
                ? await _context.Set<T>().AsNoTracking().SingleOrDefaultAsync(filter)
                : await _context.Set<T>().SingleOrDefaultAsync(filter);
        }

        public async Task<IQueryable<T>> GetQuery()
        {
            var data = await _context.Set<T>().ToListAsync();
            var quarabled = data.AsQueryable();
            return _context.Set<T>().AsQueryable();
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<T>> CreateRangeAsync(List<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();
            return entities;
        }

        public async Task RemoveRange(List<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
            await _context.SaveChangesAsync();
        }
    }
}
