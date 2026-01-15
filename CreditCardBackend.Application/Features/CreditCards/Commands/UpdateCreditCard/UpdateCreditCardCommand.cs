using ErrorOr;
using MediatR;

namespace CreditCardBackend.Application.Features.CreditCards.Commands.UpdateCreditCard
{
    public record UpdateCreditCardCommand(
        Guid Id,
        string CardHolderName,
        string ExpirationMonth,
        string ExpirationYear) : IRequest<ErrorOr<bool>>;
}
