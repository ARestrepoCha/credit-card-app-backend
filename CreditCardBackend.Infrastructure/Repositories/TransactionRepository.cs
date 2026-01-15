using CreditCardBackend.Domain.Entities;
using CreditCardBackend.Domain.Interfaces;
using CreditCardBackend.Infrastructure.Persistence;
using CreditCardBackend.Infrastructure.Repositories.Generic;

namespace CreditCardBackend.Infrastructure.Repositories
{
    public class TransactionRepository(AppDbContext appDbContext) : BaseRepository<Transaction>(appDbContext), ITransactionRepository
    {
    }
}
