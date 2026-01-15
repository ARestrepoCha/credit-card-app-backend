namespace CreditCardBackend.Application.Features.Payments.Dtos
{
    public record ProcessPaymentRequestDto(Guid CreditCardId, decimal Amount, string ProductDescription);
}
