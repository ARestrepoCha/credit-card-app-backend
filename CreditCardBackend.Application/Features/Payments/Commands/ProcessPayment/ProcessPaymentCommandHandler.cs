using CreditCardBackend.Domain.Entities;
using CreditCardBackend.Domain.Interfaces.IGeneric;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CreditCardBackend.Application.Features.Payments.Commands.ProcessPayment
{
    public class ProcessPaymentCommandHandler(IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor) : IRequestHandler<ProcessPaymentCommand, ErrorOr<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));

        public async Task<ErrorOr<bool>> Handle(ProcessPaymentCommand request, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Error.Unauthorized("User.NotFound", "Usuario no identificado.");
            }

            var userId = Guid.Parse(userIdClaim);

            var card = await _unitOfWork.CreditCardRepository.GetByIdAsync(request.CreditCardId);
            if (card == null)
            {
                return Error.NotFound("Card.NotFound", "La tarjeta no existe.");
            }

            if (card.UserId != userId)
            {
                return Error.Forbidden("Card.Forbidden", "No tienes permiso para editar esta tarjeta.");
            }

            if (!card.IsActive)
            {
                return Error.Validation("Card.Inactive", "No puedes pagar con una tarjeta desactivada.");
            }

            var transaction = new Transaction
            {
                CreditCardId = card.Id,
                Amount = request.Amount,
                ProductDescription = request.ProductDescription,
                TransactionDate = DateTime.UtcNow,
                Status = "Success",
                CreatedOn = DateTime.UtcNow,
                CreatedBy = userId
            };

            await _unitOfWork.Repository<Transaction>().CreateAsync(transaction);
            await _unitOfWork.Complete();

            return true;
        }
    }
}
