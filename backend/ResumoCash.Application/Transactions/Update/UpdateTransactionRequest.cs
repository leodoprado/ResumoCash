using System;
using System.Collections.Generic;
using System.Text;

namespace ResumoCash.Application.Transactions.Update;

public record UpdateTransactionRequest
(
    Guid CategoryId,
    string Description,
    decimal Amount,
    DateOnly CompetenceMonth,
    DateOnly? DueDate
);
