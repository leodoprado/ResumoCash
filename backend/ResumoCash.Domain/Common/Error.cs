namespace ResumoCash.Domain.Common;

public sealed record AppError(
    string Code,
    string Message,
    ErrorType Type)
{
    public static AppError Validation(string code, string message)
        => new(code, message, ErrorType.Validation);

    public static AppError NotFound(string code, string message)
        => new(code, message, ErrorType.NotFound);

    public static AppError Conflict(string code, string message)
        => new(code, message, ErrorType.Conflict);

    public static AppError Forbidden(string code, string message)
        => new(code, message, ErrorType.Forbidden);
}
