using ResumoCash.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResumoCash.Application.Transactions.Completed;

public class CompletedTransactionService
{
    private readonly ITransactionRepository _completedTransactionRepository;

    public CompletedTransactionService(ITransactionRepository completedTransactionRepository)
    {
        _completedTransactionRepository = completedTransactionRepository;
    }

    public async Task ExecuteAsync(
        Guid userId,
        Guid transactionId,
        CancellationToken cancellationToken = default)
    {
        var transaction = await _completedTransactionRepository.GetByIdAsync(
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

        transaction.Concluir();

        await _completedTransactionRepository.SaveChangesAsync(
            cancellationToken
        );
    }
}
