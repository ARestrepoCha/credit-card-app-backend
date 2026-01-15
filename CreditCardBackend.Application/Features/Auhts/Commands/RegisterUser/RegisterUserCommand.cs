using ErrorOr;
using MediatR;

namespace CreditCardBackend.Application.Features.Auhts.Commands.RegisterUser
{
    public record RegisterUserCommand(
        string FullName,
        string Email,
        string Password,
        string PasswordConfirmation) : IRequest<ErrorOr<bool>>;
}
