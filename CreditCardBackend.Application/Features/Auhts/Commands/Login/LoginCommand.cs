using CreditCardBackend.Application.Features.Auhts.Dtos;
using ErrorOr;
using MediatR;

namespace CreditCardBackend.Application.Features.Auhts.Commands.Login
{
    public record LoginCommand(
        string Email,
        string Password) : IRequest<ErrorOr<AuthResponseDto>>;
}
