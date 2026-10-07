using ResumoCash.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResumoCash.Domain.Entities;

public class Competence
{
    public Competence(Guid userId, DateOnly month)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Month = month;
        Status = CompetenceStatus.Open;
        CreatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public DateOnly Month { get; private set; }
    public CompetenceStatus Status { get; private set; }
    public DateTime? ClosedAt { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public User User { get; private set; } = null!;
    public ICollection<Transaction> Transactions { get; private set; } = [];

    public void Close()
    {
        if (Status == CompetenceStatus.Closed)
            throw new InvalidOperationException("A competência já está fechada.");

        Status = CompetenceStatus.Closed;
        ClosedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Reopen()
    {
        if (Status == CompetenceStatus.Open)
            throw new InvalidOperationException("A competência já está aberta.");

        Status = CompetenceStatus.Open;
        ClosedAt = null;
        UpdatedAt = DateTime.UtcNow;
    }
}
