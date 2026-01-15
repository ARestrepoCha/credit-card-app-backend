using FluentValidation;

namespace CreditCardBackend.Application.Features.Auhts.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("El nombre completo es requerido.")
                .MaximumLength(150).WithMessage("El nombre no puede exceder los 150 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El correo electrónico es requerido.")
                .EmailAddress().WithMessage("El formato del correo no es válido.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es requerida.")
                .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres.")
                .Matches(@"[A-Z]").WithMessage("La contraseña debe contener al menos una letra mayúscula.")
                .Matches(@"[a-z]").WithMessage("La contraseña debe contener al menos una letra minúscula.")
                .Matches(@"[0-9]").WithMessage("La contraseña debe contener al menos un número.")
                .Matches(@"[\!\?\*\.]").WithMessage("La contraseña debe contener al menos un carácter especial (!?*.).");

            RuleFor(x => x.PasswordConfirmation)
                .Equal(x => x.Password).WithMessage("Las contraseñas no coinciden.");
        }
    }
}
