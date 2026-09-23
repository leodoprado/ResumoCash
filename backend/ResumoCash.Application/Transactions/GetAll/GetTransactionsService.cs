using ResumoCash.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResumoCash.Application.Transactions.GetAll;

public class GetTransactionsService
{
    private readonly ITransactionRepository _transactionRepository;

    public GetTransactionsService(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<IEnumerable<GetTransactionsResponse>> ExecuteAsync(Guid userId, CancellationToken cancellationToken)
    {
        var transactions = await _transactionRepository.GetByUserIdAsync(userId, cancellationToken);

        return transactions.Select(transaction =>
            new GetTransactionsResponse
            (
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
            )
        );
    }

}
