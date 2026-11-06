using OneOf;
using ResumoCash.Domain.Common;
using ResumoCash.Domain.Common.Errors;
using ResumoCash.Domain.Repositories;
using Success = OneOf.Types.Success;

namespace ResumoCash.Application.Transactions.Completed;

public class CompletedTransactionService
{
    private readonly ITransactionRepository _completedTransactionRepository;

    public CompletedTransactionService(ITransactionRepository completedTransactionRepository)
    {
        _completedTransactionRepository = completedTransactionRepository;
    }

    public async Task<OneOf<Success, Error>> ExecuteAsync(
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
            return TransactionErrors.NotFound;

        transaction.Concluir();

        await _completedTransactionRepository.SaveChangesAsync(
            cancellationToken
        );

        return new Success();
    }
}
