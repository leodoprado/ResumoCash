using ResumoCash.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResumoCash.Application.Transactions.GetAll;

public record GetTransactionsResponse
(
    Guid Id,
    Guid CategoryId,
    Guid CompetenceId,
    string CategoryName,
    CategoryType CategoryType,
    string Description,
    decimal Amount,
    DateOnly? DueDate,
    TransactionProcessStatus ProcessStatus,
    TransactionStatus Status,
    DateTime? CompletedAt,
    DateTime CreatedAt
);
