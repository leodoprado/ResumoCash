using System;
using System.Collections.Generic;
using System.Text;

namespace ResumoCash.Application.Users.GetCurrent;

public record GetCurrentUserResponse(
    Guid Id,
    string Name,
    string Email,
    DateTime CreatedAt
);
