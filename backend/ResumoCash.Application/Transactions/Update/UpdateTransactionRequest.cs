using System;
using System.Collections.Generic;
using System.Text;

namespace ResumoCash.Application.Transactions.Update;

public record UpdateTransactionRequest
(
    Guid CategoryId,
    Guid CompetenceId,
    string Description,
    decimal Amount,
    DateOnly? DueDate
);
