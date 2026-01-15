using CreditCardBackend.Application.Features.CreditCards.Dtos;
using CreditCardBackend.Domain.Interfaces.IGeneric;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CreditCardBackend.Application.Features.CreditCards.Queries.GetCreditCardById
{
    public class GetCreditCardByIdQueryHandler(
    IUnitOfWork unitOfWork,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<GetCreditCardByIdQuery, ErrorOr<CreditCardResponseDto>>
    {
        public async Task<ErrorOr<CreditCardResponseDto>> Handle(GetCreditCardByIdQuery request, CancellationToken cancellationToken)
        {
            var card = await unitOfWork.CreditCardRepository.GetByIdAsync(request.Id);
            if (card == null)
            {
                return Error.NotFound("Card.NotFound", "La tarjeta no existe.");
            }

            var userId = Guid.Parse(httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (card.UserId != userId)
            {
                return Error.Forbidden("Card.Forbidden", "No tienes permiso para editar esta tarjeta.");
            }

            return new CreditCardResponseDto(
                card.Id,
                card.CardHolderName,
                $"**** **** **** {card.LastFourDigits}",
                card.ExpirationMonth,
                card.ExpirationYear,
                card.CardType,
                card.IsActive);
        }
    }
}
