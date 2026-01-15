using ErrorOr;
using MediatR;

namespace CreditCardBackend.Application.Features.CreditCards.Commands.CreateCreditCard
{
    public record CreateCreditCardCommand(
        string CardHolderName,
        string CardNumber,
        string ExpirationMonth,
        string ExpirationYear,
        string CVV) : IRequest<ErrorOr<bool>>;
}
