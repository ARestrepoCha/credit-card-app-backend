using ErrorOr;
using MediatR;

namespace CreditCardBackend.Application.Features.Payments.Commands.ProcessPayment
{
    public record ProcessPaymentCommand(
        Guid CreditCardId,
        decimal Amount,
        string ProductDescription) : IRequest<ErrorOr<bool>>;
}
