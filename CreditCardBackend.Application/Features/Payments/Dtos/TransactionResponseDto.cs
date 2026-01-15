namespace CreditCardBackend.Application.Features.Payments.Dtos
{
    public record TransactionResponseDto(
        Guid Id,
        string ProductDescription,
        decimal Amount,
        DateTime TransactionDate,
        string MaskedCardNumber,
        string CardType,
        string Status);
}
