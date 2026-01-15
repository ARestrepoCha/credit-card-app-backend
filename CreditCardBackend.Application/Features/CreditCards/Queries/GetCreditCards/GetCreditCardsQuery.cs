using CreditCardBackend.Application.Features.CreditCards.Dtos;
using ErrorOr;
using MediatR;

namespace CreditCardBackend.Application.Features.CreditCards.Queries.GetCreditCards
{
    public record GetCreditCardsQuery() : IRequest<ErrorOr<List<CreditCardResponseDto>>>;
}
