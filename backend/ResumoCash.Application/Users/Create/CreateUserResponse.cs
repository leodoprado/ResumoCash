using System;
using System.Collections.Generic;
using System.Text;

namespace ResumoCash.Application.Users.Create;

public record CreateUserResponse(
    Guid Id,
    string Name,
    string Email
);
