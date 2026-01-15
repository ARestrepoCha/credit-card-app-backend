using CreditCardBackend.Domain.Interfaces.IGeneric;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CreditCardBackend.Application.Features.CreditCards.Commands.ChangeCreditCardStatus
{
    public class ChangeCreditCardStatusHandler(
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor) : IRequestHandler<ChangeCreditCardStatusCommand, ErrorOr<bool>>
    {
        public async Task<ErrorOr<bool>> Handle(ChangeCreditCardStatusCommand request, CancellationToken cancellationToken)
        {
            var userIdClaim = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Error.Unauthorized("User.NotFound", "Usuario no identificado.");
            }

            var userId = Guid.Parse(userIdClaim);

            var card = await unitOfWork.CreditCardRepository.GetByIdAsync(request.Id);
            if (card == null)
            {
                return Error.NotFound("Card.NotFound", "La tarjeta no existe.");
            }

            if (card.UserId != userId)
            {
                return Error.Forbidden("Card.Forbidden", "No tienes permiso para editar esta tarjeta.");
            }

            card.IsActive = !card.IsActive;
            card.LastModifiedOn = DateTime.UtcNow;
            card.LastModifiedBy = userId;

            unitOfWork.CreditCardRepository.UpdateAsync(card);
            await unitOfWork.Complete();

            return true;
        }
    }
}
