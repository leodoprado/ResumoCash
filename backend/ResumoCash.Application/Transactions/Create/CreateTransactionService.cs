using ResumoCash.Domain.Entities;
using ResumoCash.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResumoCash.Application.Transactions.Create;

public class CreateTransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly ICategoryRepository _categoryRepository;

    public CreateTransactionService(ITransactionRepository transactionRepository, ICategoryRepository categoryRepository)
    {
        _transactionRepository = transactionRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<CreateTransactionResponse> ExecuteAsync(Guid userId, CreateTransactionRequest request, CancellationToken cancellationToken = default)
    {
        var category = await _categoryRepository.GetByIdAsync(
            userId,
            request.CategoryId,
            cancellationToken);

        if (category is null)
            throw new InvalidOperationException("Categoria não encontrada ou inativa.");

        var transaction = new Transaction(
            request.CategoryId,
            userId,
            request.Description,
            request.Amount,
            request.CompetenceMonth,
            request.DueDate
        );

        await _transactionRepository.AddAsync(transaction, cancellationToken);
        await _transactionRepository.SaveChangesAsync(cancellationToken);

        return new CreateTransactionResponse(
            transaction.Id,
            transaction.CategoryId,
            transaction.Description,
            transaction.Amount,
            transaction.CompetenceMonth,
            transaction.DueDate,
            transaction.ProcessStatus,
            transaction.Status
        );
    }
}
