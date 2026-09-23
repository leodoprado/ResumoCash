using System;
using System.Collections.Generic;
using System.Text;
using ResumoCash.Domain.Enums;

namespace ResumoCash.Application.Categories.Create;

public record CreateCategoryRequest(
    string Name,
    CategoryType Type
);