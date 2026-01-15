namespace CreditCardBackend.Application.Features.CreditCards.Dtos
{
    public record CreateCreditCardRequestDto(
        string CardHolderName,
        string CardNumber,
        string ExpirationMonth,
        string ExpirationYear,
        string CVV);
}
