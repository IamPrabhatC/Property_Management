using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace PropertyManagement.Infrastructure.Repositories
{
    /// <summary>
    /// Repository Base class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class RepositoryBase<T> where T : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;

        public RepositoryBase(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
      

        // Get entity by ID
        public virtual async Task<T?> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        // Find entities by condition
        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        // Add a new entity
        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        // Update an entity
        public virtual void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        // Delete an entity
        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
