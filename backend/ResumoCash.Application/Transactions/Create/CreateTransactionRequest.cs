using System;
using System.Collections.Generic;
using System.Text;

namespace ResumoCash.Application.Transactions.Create;

public record CreateTransactionRequest
(
    Guid CategoryId,
    Guid CompetenceId,
    string Description,
    decimal Amount,
    DateOnly? DueDate
);
