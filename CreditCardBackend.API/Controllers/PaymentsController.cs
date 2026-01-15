using CreditCardBackend.API.Common.Responses;
using CreditCardBackend.Application.Common.Models;
using CreditCardBackend.Application.Features.Payments.Commands.ProcessPayment;
using CreditCardBackend.Application.Features.Payments.Dtos;
using CreditCardBackend.Application.Features.Payments.Queries.GetTransactionHistory;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CreditCardBackend.API.Controllers
{
    public class PaymentsController(IMediator mediator) : ApiController
    {
        private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        [HttpPost]
        public async Task<IActionResult> ProcessPayment([FromBody] ProcessPaymentRequestDto request)
        {
            var command = new ProcessPaymentCommand(
                request.CreditCardId,
                request.Amount,
                request.ProductDescription);

            var result = await _mediator.Send(command);

            return result.Match(
                payment => StatusCode(
                    StatusCodes.Status200OK,
                    ApiResponse<bool>.Success(HttpStatusCode.OK, payment)
                ), errors => Problem(errors)
            );
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetHistory([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _mediator.Send(new GetTransactionHistoryQuery(pageNumber, pageSize));

            return result.Match(
                payments => StatusCode(
                    StatusCodes.Status200OK,
                    ApiResponse<PagedList<TransactionResponseDto>>.Success(HttpStatusCode.OK, payments)
                ), errors => Problem(errors)
            );
        }
    }
}
