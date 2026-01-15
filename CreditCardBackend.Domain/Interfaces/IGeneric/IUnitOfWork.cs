using CreditCardBackend.Domain.Interfaces.Repositories;

namespace CreditCardBackend.Domain.Interfaces.IGeneric
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<T> Repository<T>() where T : class;
        ICreditCardRepository CreditCardRepository { get; }

        Task<int> Complete();
    }
}
