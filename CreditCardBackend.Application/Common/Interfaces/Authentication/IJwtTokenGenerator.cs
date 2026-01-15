using CreditCardBackend.Domain.Entities;

namespace CreditCardBackend.Application.Common.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
