using CreditCardBackend.API.Common.Responses;
using CreditCardBackend.Application.Features.CreditCards.Commands.ChangeCreditCardStatus;
using CreditCardBackend.Application.Features.CreditCards.Commands.CreateCreditCard;
using CreditCardBackend.Application.Features.CreditCards.Commands.UpdateCreditCard;
using CreditCardBackend.Application.Features.CreditCards.Dtos;
using CreditCardBackend.Application.Features.CreditCards.Queries.GetCreditCardById;
using CreditCardBackend.Application.Features.CreditCards.Queries.GetCreditCards;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CreditCardBackend.API.Controllers
{
    public class CreditCardsController(IMediator mediator) : ApiController
    {
        private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCreditCardRequestDto request)
        {
            var command = new CreateCreditCardCommand(
                request.CardHolderName,
                request.CardNumber,
                request.ExpirationMonth,
                request.ExpirationYear,
                request.CVV);

            var result = await _mediator.Send(command);

            return result.Match(
                creditCard => StatusCode(
                    StatusCodes.Status200OK,
                    ApiResponse<bool>.Success(HttpStatusCode.OK, creditCard)
                ), errors => Problem(errors)
            );
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCreditCardRequestDto request)
        {
            var command = new UpdateCreditCardCommand(
                id,
                request.CardHolderName,
                request.ExpirationMonth,
                request.ExpirationYear);

            var result = await _mediator.Send(command);

            return result.Match(
                creditCard => StatusCode(
                    StatusCodes.Status200OK,
                    ApiResponse<bool>.Success(HttpStatusCode.OK, creditCard)
                ), errors => Problem(errors)
            );
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetCreditCardsQuery());

            return result.Match(
                cards => Ok(ApiResponse<List<CreditCardResponseDto>>.Success(HttpStatusCode.OK, cards)),
                errors => Problem(errors)
            );
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetCreditCardByIdQuery(id));

            return result.Match(
                card => Ok(ApiResponse<CreditCardResponseDto>.Success(HttpStatusCode.OK, card)),
                errors => Problem(errors)
            );
        }

        [HttpPatch("{id:guid}/status")]
        public async Task<IActionResult> ChangeStatus(Guid id)
        {
            var result = await _mediator.Send(new ChangeCreditCardStatusCommand(id));

            return result.Match(
                isActive => Ok(ApiResponse<bool>.Success(
                    HttpStatusCode.OK,
                    isActive,
                    $"La tarjeta ha sido {(isActive ? "activada" : "desactivada")} correctamente.")),
                errors => Problem(errors)
            );
        }
    }
}
