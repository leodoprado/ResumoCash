using ResumoCash.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResumoCash.Application.Transactions.GetById;

public record GetTransactionByIdResponse
(
    Guid Id,
    Guid CategoryId,
    Guid CompetenceId,
    string CategoryName,
    string Description,
    decimal Amount,
    DateOnly? DueDate,
    TransactionProcessStatus ProcessStatus,
    TransactionStatus Status,
    DateTime? CompletedAt,
    DateTime CreatedAt
);
