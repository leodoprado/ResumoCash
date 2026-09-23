using ResumoCash.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResumoCash.Application.Categories.GetAll;

public record GetCategoriesResponse(
    Guid Id,
    string Name,
    CategoryType Type,
    CategoryStatus Status
);
