namespace CreditCardBackend.Application.Features.CreditCards.Dtos
{
    public record CreditCardResponseDto(
        Guid Id,
        string CardHolderName,
        string MaskedNumber,
        string ExpirationMonth,
        string ExpirationYear,
        string CardType,
        bool IsActive);
}
