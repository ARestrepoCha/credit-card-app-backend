using CreditCardBackend.Application.Features.CreditCards.Dtos;
using ErrorOr;
using MediatR;

namespace CreditCardBackend.Application.Features.CreditCards.Queries.GetCreditCardById
{
    public record GetCreditCardByIdQuery(Guid Id) : IRequest<ErrorOr<CreditCardResponseDto>>;
}
