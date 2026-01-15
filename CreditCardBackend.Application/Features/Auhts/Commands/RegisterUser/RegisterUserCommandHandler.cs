using CreditCardBackend.Domain.Entities;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CreditCardBackend.Application.Features.Auhts.Commands.RegisterUser
{
    public class RegisterUserCommandHandler(UserManager<User> userManager)
    : IRequestHandler<RegisterUserCommand, ErrorOr<bool>>
    {
        public async Task<ErrorOr<bool>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if (request.Password != request.PasswordConfirmation)
            {
                return Error.Validation("Validation", "Las contraseñas no coinciden.");
            }

            var existingUser = await userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return Error.Validation("Validation", "El correo electrónico ya está en uso.");
            }

            var user = new User
            {
                UserName = request.Email,
                Email = request.Email,
                FullName = request.FullName,
                IsActive = true,
                CreatedOn = DateTime.UtcNow
            };

            var result = await userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                return true;
            }

            var errors = result.Errors.Select(e => Error.Validation(e.Code, e.Description)).ToList();
            return errors;
        }
    }
}
