using OneOf;
using ResumoCash.Domain.Common;

namespace ResumoCash.Api.Common.Extensions;

public static class OneOfExtensions
{
    public static bool IsError<TResult>(this OneOf<TResult, AppError> obj) => obj.IsT1;
    public static AppError GetErrorResult<TResult>(this OneOf<TResult, AppError> obj) => obj.AsT1;
    public static TResult GetSuccessResult<TResult>(this OneOf<TResult, AppError> obj) => obj.AsT0;
}
