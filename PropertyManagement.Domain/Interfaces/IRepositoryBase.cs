using System.Linq.Expressions;

namespace PropertyManagement.Domain.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        Task AddAsync(T entity);
        Task<T?> GetByIdAsync(object id);     
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        void Update(T entity);
        void Delete(T entity);
    }
}
