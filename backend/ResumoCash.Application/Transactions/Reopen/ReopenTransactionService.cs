using OneOf;
using ResumoCash.Domain.Common;
using ResumoCash.Domain.Common.Errors;
using ResumoCash.Domain.Repositories;
using Success = OneOf.Types.Success;

namespace ResumoCash.Application.Transactions.Reopen;

public class ReopenTransactionService
{
    private readonly ITransactionRepository _reopenTransactionRepository;

    public ReopenTransactionService(ITransactionRepository reopenTransactionRepository)
    {
        _reopenTransactionRepository = reopenTransactionRepository;
    }

    public async Task<OneOf<Success, AppError>> ExecuteAsync(
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
            return TransactionErrors.NotFound;

        transaction.Reabrir();

        await _reopenTransactionRepository.SaveChangesAsync(cancellationToken);

        return new Success();
    }

}
