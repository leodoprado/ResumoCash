using System;
using System.Collections.Generic;
using System.Text;

namespace ResumoCash.Application.Transactions.Create;

public record CreateTransactionRequest
(
    Guid CategoryId,
    string Description,
    decimal Amount,
    DateOnly CompetenceMonth,
    DateOnly? DueDate
);
