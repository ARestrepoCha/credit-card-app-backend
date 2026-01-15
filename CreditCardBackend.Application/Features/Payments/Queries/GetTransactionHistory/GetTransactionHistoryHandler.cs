using CreditCardBackend.Application.Common.Models;
using CreditCardBackend.Application.Features.Payments.Dtos;
using CreditCardBackend.Domain.Entities;
using CreditCardBackend.Domain.Interfaces.IGeneric;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CreditCardBackend.Application.Features.Payments.Queries.GetTransactionHistory
{
    public class GetTransactionHistoryHandler(
    IUnitOfWork unitOfWork,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<GetTransactionHistoryQuery, ErrorOr<PagedList<TransactionResponseDto>>>
    {
        public async Task<ErrorOr<PagedList<TransactionResponseDto>>> Handle(GetTransactionHistoryQuery request, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var (items, totalCount) = await unitOfWork.Repository<Transaction>().GetPagedAsync(
                t => t.CreditCard.UserId == userId,
                request.PageNumber,
                request.PageSize,
                t => t.CreditCard
            );

            var result = items.Select(t => new TransactionResponseDto(
                t.Id,
                t.ProductDescription,
                t.Amount,
                t.TransactionDate,
                $"**** **** **** {t.CreditCard.LastFourDigits}",
                t.CreditCard.CardType,
                t.Status
            )).ToList();

            return new PagedList<TransactionResponseDto>(result, totalCount, request.PageNumber, request.PageSize);
        }
    }
}
