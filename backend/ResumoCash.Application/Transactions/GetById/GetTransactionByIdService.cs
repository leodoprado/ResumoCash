using OneOf;
using ResumoCash.Domain.Common;
using ResumoCash.Domain.Common.Errors;
using ResumoCash.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResumoCash.Application.Transactions.GetById;

public class GetTransactionByIdService
{
    private readonly ITransactionRepository _transactionRepository;

    public GetTransactionByIdService(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<OneOf<GetTransactionByIdResponse, AppError>> ExecuteAsync(
        Guid userId,
        Guid transactionId,
        CancellationToken cancellationToken)
    {
        var transaction = await _transactionRepository.GetByIdAsync(userId, transactionId, cancellationToken);
        
        if (transaction is null || transaction.UserId != userId)
            return TransactionErrors.NotFound;

        return new GetTransactionByIdResponse(
            transaction.Id,
            transaction.CategoryId,
            transaction.CompetenceId,
            transaction.Category.Name,
            transaction.Description,
            transaction.Amount,
            transaction.DueDate,
            transaction.ProcessStatus,
            transaction.Status,
            transaction.CompletedAt,
            transaction.CreatedAt
        );
    }
}
