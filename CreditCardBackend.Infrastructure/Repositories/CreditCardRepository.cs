using CreditCardBackend.Domain.Entities;
using CreditCardBackend.Domain.Interfaces.Repositories;
using CreditCardBackend.Infrastructure.Persistence;
using CreditCardBackend.Infrastructure.Repositories.Generic;

namespace CreditCardBackend.Infrastructure.Repositories
{
    public class CreditCardRepository(AppDbContext appDbContext) : BaseRepository<CreditCard>(appDbContext), ICreditCardRepository
    {
    }
}
