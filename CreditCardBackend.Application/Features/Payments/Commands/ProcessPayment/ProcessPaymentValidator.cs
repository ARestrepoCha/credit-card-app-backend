using FluentValidation;

namespace CreditCardBackend.Application.Features.Payments.Commands.ProcessPayment
{
    public class ProcessPaymentValidator : AbstractValidator<ProcessPaymentCommand>
    {
        public ProcessPaymentValidator()
        {
            RuleFor(x => x.CreditCardId)
                .NotEmpty().WithMessage("El ID de la tarjeta es requerido.");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("El monto del pago debe ser mayor a cero.")
                .PrecisionScale(18, 2, false).WithMessage("El formato del monto no es válido.");

            RuleFor(x => x.ProductDescription)
                .NotEmpty().WithMessage("La descripción del producto es requerida.")
                .MaximumLength(250).WithMessage("La descripción no puede exceder los 250 caracteres.")
                .MinimumLength(3).WithMessage("La descripción debe tener al menos 3 caracteres.");
        }
    }
}
