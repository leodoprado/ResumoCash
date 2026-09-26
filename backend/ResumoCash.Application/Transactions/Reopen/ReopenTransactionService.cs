using ResumoCash.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResumoCash.Application.Transactions.Reopen;

public class ReopenTransactionService
{
    private readonly ITransactionRepository _reopenTransactionRepository;

    public ReopenTransactionService(ITransactionRepository reopenTransactionRepository)
    {
        _reopenTransactionRepository = reopenTransactionRepository;
    }

    public async Task ExecuteAsync(
        Guid userId,
        Guid transactionId,
        CancellationToken cancellationToken = default)
    {
        var transaction = await _reopenTransactionRepository.GetByIdAsync(
            userId,
            transactionId,
            cancellationToken
        );

        if (transaction is null || transaction.UserId != userId)
        {
            throw new InvalidOperationException(
                "Transação não encontrada."
            );
        }

        transaction.Reabrir();

        await _reopenTransactionRepository.SaveChangesAsync(
            cancellationToken
        );
    }

}
