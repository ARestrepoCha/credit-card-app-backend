namespace CreditCardBackend.Application.Features.Auhts.Dtos
{
    public record AuthResponseDto(
        Guid Id,
        string FullName,
        string Email,
        string Token);
}
