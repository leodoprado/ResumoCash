using System;
using System.Collections.Generic;
using System.Text;

namespace ResumoCash.Domain.Common;

public enum ErrorType
{
    Validation,
    NotFound,
    Conflict,
    Forbidden
}
