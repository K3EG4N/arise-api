using System.Linq.Expressions;
using arise_api.provider;
using Microsoft.EntityFrameworkCore;

namespace arise_api.generic
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T?> GetFirstAsync(Expression<Func<T, bool>> predicate);
    }

    public class BaseRepository<T>(AriseDbContext context) : IBaseRepository<T> where T : class
    {
        public readonly AriseDbContext _context = context;

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<T?> GetFirstAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
