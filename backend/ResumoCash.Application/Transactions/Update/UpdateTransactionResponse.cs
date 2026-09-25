using ResumoCash.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResumoCash.Application.Transactions.Update;

public record UpdateTransactionResponse
(
    Guid Id,
    Guid CategoryId,
    string Description,
    decimal Amount,
    DateOnly CompetenceMonth,
    DateOnly? DueDate,
    TransactionProcessStatus ProcessStatus,
    TransactionStatus Status,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
