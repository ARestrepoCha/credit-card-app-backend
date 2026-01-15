using FluentValidation;

namespace CreditCardBackend.Application.Features.CreditCards.Commands.CreateCreditCard
{
    public class CreateCreditCardValidator : AbstractValidator<CreateCreditCardCommand>
    {
        public CreateCreditCardValidator()
        {
            RuleFor(x => x.CardHolderName).NotEmpty().MaximumLength(100);

            RuleFor(x => x.CardNumber)
                .NotEmpty()
                .CreditCard().WithMessage("Número de tarjeta inválido.")
                .Length(13, 19);

            RuleFor(x => x.CVV)
                .NotEmpty()
                .Matches(@"^\d{3,4}$").WithMessage("El CVV debe tener 3 o 4 dígitos.");

            RuleFor(x => x.ExpirationMonth)
                .NotEmpty()
                .Matches(@"^(0[1-9]|1[0-2])$").WithMessage("Mes inválido (01-12).");

            // Validación de año: que no sea menor al actual
            RuleFor(x => x.ExpirationYear)
                .NotEmpty()
                .Must(y => int.Parse(y) >= DateTime.Now.Year % 100)
                .WithMessage("La tarjeta está expirada.");
        }
    }
}
