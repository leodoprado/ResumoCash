using Microsoft.AspNetCore.Mvc;
using ResumoCash.Domain.Common;

namespace ResumoCash.Api.Common.Extensions;

public static class ErrorExtensions
{
    public static IActionResult ToActionResult(
        this Error error,
        ControllerBase controller)
    {
        return error.Type switch
        {
            ErrorType.Validation => controller.BadRequest(error),
            ErrorType.NotFound => controller.NotFound(error),
            ErrorType.Conflict => controller.Conflict(error),
            ErrorType.Forbidden => controller.StatusCode(403, error),
            _ => controller.StatusCode(500, error)
        };
    }
}
