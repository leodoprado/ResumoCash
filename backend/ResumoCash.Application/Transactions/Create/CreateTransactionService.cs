using OneOf;
using ResumoCash.Domain.Common;
using ResumoCash.Domain.Common.Errors;
using ResumoCash.Domain.Entities;
using ResumoCash.Domain.Repositories;

namespace ResumoCash.Application.Transactions.Create;

public class CreateTransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICompetenceRepository _competenceRepository;

    public CreateTransactionService(ITransactionRepository transactionRepository, ICategoryRepository categoryRepository, ICompetenceRepository competenceRepository)
    {
        _transactionRepository = transactionRepository;
        _categoryRepository = categoryRepository;
        _competenceRepository = competenceRepository;
    }

    public async Task<OneOf<CreateTransactionResponse, AppError>> ExecuteAsync(Guid userId, CreateTransactionRequest request, CancellationToken cancellationToken = default)
    {
        var category = await _categoryRepository.GetByIdAsync(
            userId,
            request.CategoryId,
            cancellationToken);

        if (category is null)
            return TransactionErrors.CategoryNotFound;

        var competence = await _competenceRepository.GetByIdAsync(
            userId,
            request.CompetenceId,
            cancellationToken);

        if (competence is null)
            return TransactionErrors.CompetenceNotFound;

        var transaction = new Transaction(
            request.CategoryId,
            userId,
            request.Description,
            request.Amount,
            request.CompetenceId,
            request.DueDate
        );

        await _transactionRepository.AddAsync(transaction, cancellationToken);
        await _transactionRepository.SaveChangesAsync(cancellationToken);

        return new CreateTransactionResponse(
            transaction.Id,
            transaction.CategoryId,
            transaction.CompetenceId,
            transaction.Description,
            transaction.Amount,
            transaction.DueDate,
            transaction.ProcessStatus,
            transaction.Status
        );
    }
}
