using CreditCardBackend.Application.Common.Interfaces.Security;
using CreditCardBackend.Domain.Entities;
using CreditCardBackend.Domain.Interfaces.IGeneric;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CreditCardBackend.Application.Features.CreditCards.Commands.CreateCreditCard
{
    public class CreateCreditCardCommandHandler(
        IUnitOfWork unitOfWork,
        IEncryptionService encryptionService,
        IHttpContextAccessor httpContextAccessor) : IRequestHandler<CreateCreditCardCommand, ErrorOr<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        private readonly IEncryptionService _encryptionService = encryptionService ?? throw new ArgumentNullException(nameof(encryptionService));
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));

        public async Task<ErrorOr<bool>> Handle(CreateCreditCardCommand request, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Error.Unauthorized("User.NotFound", "Usuario no identificado.");
            }

            var userId = Guid.Parse(userIdClaim);
            string cardType = GetCardType(request.CardNumber);

            var creditCard = new CreditCard
            {
                CardHolderName = request.CardHolderName,
                EncryptedCardNumber = _encryptionService.Encrypt(request.CardNumber),
                LastFourDigits = request.CardNumber[^4..],
                ExpirationMonth = request.ExpirationMonth,
                ExpirationYear = request.ExpirationYear,
                CardType = cardType,
                UserId = userId,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = userId,
                IsActive = true
            };

            await _unitOfWork.CreditCardRepository.CreateAsync(creditCard);
            await _unitOfWork.Complete();

            return true;
        }

        private static string GetCardType(string cardNumber)
        {
            if (cardNumber.StartsWith("4")) return "Visa";
            if (cardNumber.StartsWith("5")) return "Mastercard";
            if (cardNumber.StartsWith("34") || cardNumber.StartsWith("37")) return "Amex";
            return "Unknown";
        }
    }
}
