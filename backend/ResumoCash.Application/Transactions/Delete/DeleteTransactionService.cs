using OneOf;
using ResumoCash.Domain.Common;
using ResumoCash.Domain.Common.Errors;
using ResumoCash.Domain.Repositories;
using Success = OneOf.Types.Success;

namespace ResumoCash.Application.Transactions.Delete;

public class DeleteTransactionService
{
    private readonly ITransactionRepository _transactionRepository;

    public DeleteTransactionService(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<OneOf<Success, AppError>> ExecuteAsync(
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
            return TransactionErrors.NotFound;

        transaction.Inativar();

        await _transactionRepository.SaveChangesAsync(
            cancellationToken
        );

        return new Success();
    }
}
