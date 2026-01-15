using CreditCardBackend.Application.Features.CreditCards.Dtos;
using CreditCardBackend.Domain.Interfaces.IGeneric;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CreditCardBackend.Application.Features.CreditCards.Queries.GetCreditCards
{
    public class GetCreditCardsQueryHandler(
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor) : IRequestHandler<GetCreditCardsQuery, ErrorOr<List<CreditCardResponseDto>>>
    {
        public async Task<ErrorOr<List<CreditCardResponseDto>>> Handle(GetCreditCardsQuery request, CancellationToken cancellationToken)
        {
            var userIdClaim = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Error.Unauthorized();
            var userId = Guid.Parse(userIdClaim);

            var cards = await unitOfWork.CreditCardRepository.GetAsync(c => c.UserId == userId);

            var response = cards.Select(c => new CreditCardResponseDto(
                c.Id,
                c.CardHolderName,
                $"**** **** **** {c.LastFourDigits}",
                c.ExpirationMonth,
                c.ExpirationYear,
                c.CardType,
                c.IsActive
            )).ToList();

            return response;
        }
    }
}
