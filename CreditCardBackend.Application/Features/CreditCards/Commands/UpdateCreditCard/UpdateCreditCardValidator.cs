using FluentValidation;

namespace CreditCardBackend.Application.Features.CreditCards.Commands.UpdateCreditCard
{
    public class UpdateCreditCardValidator : AbstractValidator<UpdateCreditCardCommand>
    {
        public UpdateCreditCardValidator()
        {
            RuleFor(x => x.Id).NotEmpty();

            RuleFor(x => x.CardHolderName)
                .NotEmpty().MaximumLength(100);

            RuleFor(x => x.ExpirationMonth)
                .NotEmpty().Matches(@"^(0[1-9]|1[0-2])$");

            RuleFor(x => x.ExpirationYear)
                .NotEmpty().Must(y => int.Parse(y) >= DateTime.Now.Year % 100);
        }
    }
}
