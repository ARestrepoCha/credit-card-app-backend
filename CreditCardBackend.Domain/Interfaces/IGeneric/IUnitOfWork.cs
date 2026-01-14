namespace CreditCardBackend.Domain.Interfaces.IGeneric
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<T> Repository<T>() where T : class;
        Task<int> Complete();
    }
}
