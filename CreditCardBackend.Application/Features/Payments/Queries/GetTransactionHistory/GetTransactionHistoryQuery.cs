using CreditCardBackend.Application.Common.Models;
using CreditCardBackend.Application.Features.Payments.Dtos;
using ErrorOr;
using MediatR;

namespace CreditCardBackend.Application.Features.Payments.Queries.GetTransactionHistory
{
    public record GetTransactionHistoryQuery(int PageNumber = 1, int PageSize = 10)
        : IRequest<ErrorOr<PagedList<TransactionResponseDto>>>;
}
