using BusApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BusApi.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(ApplicationContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbSet.FindAsync([id], cancellationToken);
        }

        public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbSet.ToListAsync(cancellationToken);
        }

        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _dbSet.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task CreateAsync(T entity, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}