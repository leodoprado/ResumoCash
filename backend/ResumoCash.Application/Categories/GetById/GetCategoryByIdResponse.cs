using ResumoCash.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResumoCash.Application.Categories.GetById;

public record GetCategoryByIdResponse(
    Guid Id,
    string Name,
    CategoryType Type,
    CategoryStatus Status,
    DateTime CreatedAt
);
