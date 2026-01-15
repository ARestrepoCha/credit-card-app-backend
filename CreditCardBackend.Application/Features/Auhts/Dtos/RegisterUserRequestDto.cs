namespace CreditCardBackend.Application.Features.Auhts.Dtos
{
    public record RegisterUserRequestDto(
        string? FullName,
        string? Email,
        string? Password,
        string? PasswordConfirmation);
}
