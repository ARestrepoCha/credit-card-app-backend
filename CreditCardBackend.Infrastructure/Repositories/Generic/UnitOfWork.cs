using CreditCardBackend.Domain.Interfaces.IGeneric;
using CreditCardBackend.Infrastructure.Persistence;
using System.Collections;

namespace CreditCardBackend.Infrastructure.Repositories.Generic
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        private readonly AppDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
        private Hashtable? _repositories;
        private bool _disposed = false;

        public IBaseRepository<T> Repository<T>() where T : class
        {
            _repositories ??= [];
            var type = typeof(T).Name;
            if (!_repositories.ContainsKey(type))
            {
                var respositoryType = typeof(BaseRepository<>);
                var respositoryInstance = Activator.CreateInstance(respositoryType.MakeGenericType(typeof(T)), _context);
                _repositories.Add(type, respositoryInstance);
            }
            return (IBaseRepository<T>)_repositories[type]!;
        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _context.Dispose();
            }

            _disposed = true;
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}
