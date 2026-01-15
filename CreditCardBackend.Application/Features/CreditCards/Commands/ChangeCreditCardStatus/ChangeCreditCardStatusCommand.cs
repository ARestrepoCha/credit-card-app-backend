using ErrorOr;
using MediatR;

namespace CreditCardBackend.Application.Features.CreditCards.Commands.ChangeCreditCardStatus
{
    public record ChangeCreditCardStatusCommand(Guid Id) : IRequest<ErrorOr<bool>>;
}
