using System;
using System.Collections.Generic;
using System.Text;

namespace ResumoCash.Application.Users.Login;

public record LoginUserResponse(
    string AccessToken,
    string TokenType,
    int ExpiresIn
);
