using CreditCardBackend.Application.Common.Interfaces.Authentication;
using CreditCardBackend.Application.Features.Auhts.Dtos;
using CreditCardBackend.Domain.Entities;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CreditCardBackend.Application.Features.Auhts.Commands.Login
{
    public class LoginCommandHandler(
        UserManager<User> userManager,
        IJwtTokenGenerator jwtTokenGenerator) : IRequestHandler<LoginCommand, ErrorOr<AuthResponseDto>>
    {
        private readonly UserManager<User> _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator ?? throw new ArgumentNullException(nameof(jwtTokenGenerator));

        public async Task<ErrorOr<AuthResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
            {
                return Error.Validation("Validation", "Credenciales inválidas.");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!isPasswordValid)
            {
                return Error.Validation("Validation", "Credenciales inválidas.");
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthResponseDto(
                user.Id,
                user.FullName,
                user.Email!,
                token);
        }
    }
}
