using System;
using System.Collections.Generic;
using System.Text;

namespace ResumoCash.Application.Categories.Create;

public record CreateCategoryResponse(
    Guid Id,
    string Name
);