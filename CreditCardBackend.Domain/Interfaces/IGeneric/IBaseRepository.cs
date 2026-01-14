using System.Linq.Expressions;

namespace CreditCardBackend.Domain.Interfaces.IGeneric
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(params string[] includeProperties);
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetQueryable();
        IQueryable<T> GetQueryable(params string[] includeProperties);
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate, params string[] includeProperties);
        Task<T?> GetByIdAsync(object id);
        Task<T?> GetByIdAsync(object id, params string[] includeProperties);
        Task CreateAsync(T entity);
        void UpdateAsync(T entity);
        void DeleteAsync(T entity);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, params string[] includeProperties);
    }
}
