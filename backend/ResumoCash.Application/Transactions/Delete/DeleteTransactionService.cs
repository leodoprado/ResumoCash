using ResumoCash.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResumoCash.Application.Transactions.Delete;

public class DeleteTransactionService
{
    private readonly ITransactionRepository _transactionRepository;

    public DeleteTransactionService(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task ExecuteAsync(
        Guid userId,
        Guid transactionId,
        CancellationToken cancellationToken = default)
    {
        var transaction = await _transactionRepository.GetByIdAsync(
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

        transaction.Inativar();

        await _transactionRepository.SaveChangesAsync(
            cancellationToken
        );

    }
}
