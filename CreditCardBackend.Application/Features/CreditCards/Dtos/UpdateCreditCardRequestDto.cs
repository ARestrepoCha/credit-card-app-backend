namespace CreditCardBackend.Application.Features.CreditCards.Dtos
{
    public record UpdateCreditCardRequestDto(
        string CardHolderName,
        string ExpirationMonth,
        string ExpirationYear);
}
