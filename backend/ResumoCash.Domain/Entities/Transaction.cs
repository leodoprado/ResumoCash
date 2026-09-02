using System;
using System.Collections.Generic;
using System.Text;

namespace ResumoCash.Domain.Entities;

public class Transaction
{
    public Transaction(Guid categoryId, Guid userId, string description, decimal amount, DateOnly competenceMonth, DateOnly? dueDate)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        CategoryId = categoryId;
        Description = description;
        Amount = amount;
        CompetenceMonth = new DateOnly(competenceMonth.Year, competenceMonth.Month, 1);
        DueDate = dueDate;
        IsCompleted = false;
        CreatedAt = DateTime.Now;
    }

    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid CategoryId { get; private set; }
    public string Description { get; private set; }
    public decimal Amount { get; private set; }
    public DateOnly CompetenceMonth { get; private set; }
    public DateOnly? DueDate { get; private set; }
    public bool IsCompleted { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public User User { get; private set; } = null!;
    public Category Category { get; private set; } = null!;

    public void AtualizarDescricao(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("A descrição é obrigatória.");

        Description = description;
        UpdatedAt = DateTime.Now;
    }

    public void AtyakuzarValor(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("O valor deve ser maior que zero.");

        Amount = amount;
        UpdatedAt = DateTime.Now;
    }

    public void AtualizarVencimento(DateOnly? dueDate)
    {
        DueDate = dueDate;
        UpdatedAt = DateTime.Now;
    }

    public void Concluir()
    {
        IsCompleted = true;
        CompletedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }

    public void Reabrir()
    {
        IsCompleted = false;
        CompletedAt = null;
        UpdatedAt = DateTime.Now;
    }
}
