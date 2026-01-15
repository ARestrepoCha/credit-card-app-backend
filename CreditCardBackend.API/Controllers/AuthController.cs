using CreditCardBackend.API.Common.Responses;
using CreditCardBackend.Application.Features.Auhts.Commands.Login;
using CreditCardBackend.Application.Features.Auhts.Commands.RegisterUser;
using CreditCardBackend.Application.Features.Auhts.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CreditCardBackend.API.Controllers
{
    public class AuthController(IMediator mediator) : ApiController
    {
        private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequestDto request)
        {
            var command = new RegisterUserCommand(
                request.FullName!,
                request.Email!,
                request.Password!,
                request.PasswordConfirmation!);

            var result = await _mediator.Send(command);

            return result.Match(
                register => StatusCode(
                    StatusCodes.Status200OK,
                    ApiResponse<bool>.Success(HttpStatusCode.OK, register)
                ), errors => Problem(errors)
            );
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var command = new LoginCommand(request.Email!, request.Password!);

            var result = await _mediator.Send(command);

            return result.Match(
                login => StatusCode(
                    StatusCodes.Status200OK,
                    ApiResponse<AuthResponseDto>.Success(HttpStatusCode.OK, login)
                ), errors => Problem(errors)
            );
        }
    }
}
