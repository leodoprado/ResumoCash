namespace ResumoCash.Domain.Common.Errors;

public static class CategoryErrors
{
    public static readonly AppError AlreadyExists =
        AppError.Conflict(
            "Category.AlreadyExists",
            "Já existe uma categoria com esse nome.");

    public static readonly AppError NotFound =
        AppError.NotFound(
            "Category.NotFound",
            "Categoria não encontrada.");

    public static readonly AppError InvalidName =
        AppError.Validation(
            "Category.InvalidName",
            "O nome da categoria é obrigatório.");
}
