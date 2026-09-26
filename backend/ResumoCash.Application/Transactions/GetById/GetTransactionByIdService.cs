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

    public async Task<GetTransactionByIdResponse?> ExecuteAsync(
        Guid userId,
        Guid transactionId,
        CancellationToken cancellationToken)
    {
        var transaction = await _transactionRepository.GetByIdAsync(userId, transactionId, cancellationToken);
        
        if (transaction is null)
            return null;
        
        if (transaction.UserId != userId)
            return null;

        return new GetTransactionByIdResponse(
            transaction.Id,
            transaction.CategoryId,
            transaction.Category.Name,
            transaction.Description,
            transaction.Amount,
            transaction.CompetenceMonth,
            transaction.DueDate,
            transaction.ProcessStatus,
            transaction.Status,
            transaction.CompletedAt,
            transaction.CreatedAt
        );
    }
}
