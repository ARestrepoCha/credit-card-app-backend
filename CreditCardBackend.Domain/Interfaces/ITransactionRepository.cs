using CreditCardBackend.Domain.Entities;
using CreditCardBackend.Domain.Interfaces.IGeneric;

namespace CreditCardBackend.Domain.Interfaces
{
    public interface ITransactionRepository : IBaseRepository<Transaction>
    {
    }
}
