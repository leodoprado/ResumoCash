using ResumoCash.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResumoCash.Application.Transactions.GetById;

public record GetTransactionByIdResponse
(
    Guid Id,
    Guid CategoryId,
    string CategoryName,
    string Description,
    decimal Amount,
    DateOnly CompetenceMonth,
    DateOnly? DueDate,
    TransactionProcessStatus ProcessStatus,
    TransactionStatus Status,
    DateTime? CompletedAt,
    DateTime CreatedAt
);
