using ResumoCash.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResumoCash.Application.Categories.Update;

public record UpdateCategoryResponse(
    Guid Id,
    string Name,
    TransactionType Type,
    CategoryStatus Status
);
