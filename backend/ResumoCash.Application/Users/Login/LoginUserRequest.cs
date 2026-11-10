using System;
using System.Collections.Generic;
using System.Text;

namespace ResumoCash.Application.Users.Login;

public record LoginUserRequest(
    string Email,
    string Password
);
