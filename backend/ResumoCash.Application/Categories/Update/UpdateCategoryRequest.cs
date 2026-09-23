using ResumoCash.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResumoCash.Application.Categories.Update;

public record UpdateCategoryRequest(
    string Name,
    CategoryType Type,
    CategoryStatus Status
);
