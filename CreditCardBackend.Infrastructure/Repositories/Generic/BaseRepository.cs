using CreditCardBackend.Domain.Interfaces.IGeneric;
using CreditCardBackend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CreditCardBackend.Infrastructure.Repositories.Generic
{
    public class BaseRepository<T>(AppDbContext context) : IBaseRepository<T> where T : class
    {
        private readonly AppDbContext _context = context;

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await _context.Set<T>().ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync(params string[] includeProperties)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>();

                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }

                return await query.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<T?> GetByIdAsync(object id)
        {
            try
            {
                return await _context.Set<T>().FindAsync(id);
            }
            catch (Exception ex)
            {
                // Aquí agregas contexto extra
                var entityName = typeof(T).Name;
                var msg = $"Error al obtener entidad {entityName} con id {id}: {ex.Message}";

                // Log (usa tu ILogger inyectado, Console.WriteLine solo de ejemplo)
                Console.WriteLine(msg);
                Console.WriteLine(ex);

                // Vuelves a lanzar con inner exception usando una excepción más específica
                throw new InvalidOperationException(msg, ex);
            }
        }

        public async Task<T?> GetByIdAsync(object id, params string[] includeProperties)
        {
            try
            {
                var entity = await _context.Set<T>().FindAsync(id);

                if (entity == null)
                    return null;

                foreach (var includeProperty in includeProperties)
                {
                    var navigation = _context.Entry(entity).Navigation(includeProperty);

                    if (navigation != null && !navigation.IsLoaded)
                    {
                        await navigation.LoadAsync();
                    }
                }

                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await _context.Set<T>().Where(predicate).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate, params string[] includeProperties)
        {
            IQueryable<T>? query = null;
            try
            {
                query = _context.Set<T>().Where(predicate);

                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }

                return await query.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task CreateAsync(T entity)
        {
            try
            {
                await _context.Set<T>().AddAsync(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateAsync(T entity)
        {
            try
            {
                _context.Set<T>().Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteAsync(T entity)
        {
            try
            {
                _context.Set<T>().Remove(entity);
                _context.Entry(entity).State = EntityState.Deleted;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<T> GetQueryable()
        {
            try
            {
                return _context.Set<T>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<T> GetQueryable(params string[] includeProperties)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>();

                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }

                return query;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await _context.Set<T>().FirstOrDefaultAsync(predicate);
            }
            catch (Exception ex)
            {
                var query = _context.Set<T>()
                                    .Where(predicate)
                                    .ToQueryString();

                Console.WriteLine("❌ ERROR ejecutando FirstOrDefaultAsync");
                Console.WriteLine($"Entidad: {typeof(T).Name}");
                Console.WriteLine("👉 SQL generado:");
                Console.WriteLine(query);
                Console.WriteLine("👉 Excepción:");
                Console.WriteLine(ex);

                throw; // muy importante, relanza para que EF no quede inconsistente
            }
        }

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, params string[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.FirstOrDefaultAsync(predicate);
        }
    }
}
