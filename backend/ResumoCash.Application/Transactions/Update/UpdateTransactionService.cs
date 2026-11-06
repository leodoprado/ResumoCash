using OneOf;
using ResumoCash.Domain.Common;
using ResumoCash.Domain.Common.Errors;
using ResumoCash.Domain.Repositories;

namespace ResumoCash.Application.Transactions.Update;

public class UpdateTransactionService
{
    private readonly ITransactionRepository _transactionRepository;

    public UpdateTransactionService(
        ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<OneOf<UpdateTransactionResponse, Error>> ExecuteAsync(
        Guid userId,
        Guid transactionId,
        UpdateTransactionRequest request,
        CancellationToken cancellationToken = default)
    {
        var transaction = await _transactionRepository.GetByIdAsync(
            userId,
            transactionId,
            cancellationToken
        );

        if (transaction is null || transaction.UserId != userId)
            return TransactionErrors.NotFound;

        var description = request.Description.Trim();

        transaction.AtualizarCategoria(request.CategoryId);
        transaction.AtualizarDescricao(description);
        transaction.AtualizarValor(request.Amount);
        transaction.AtualizarCompetencia(request.CompetenceId);
        transaction.AtualizarVencimento(request.DueDate);

        await _transactionRepository.SaveChangesAsync(cancellationToken);

        return new UpdateTransactionResponse(
            transaction.Id,
            transaction.CategoryId,
            transaction.CompetenceId,
            transaction.Description,
            transaction.Amount,
            transaction.DueDate,
            transaction.ProcessStatus,
            transaction.Status,
            transaction.CreatedAt,
            transaction.UpdatedAt
        );
    }
}
