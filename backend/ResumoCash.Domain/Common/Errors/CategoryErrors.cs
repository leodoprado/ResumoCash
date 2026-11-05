namespace ResumoCash.Domain.Common.Errors;

public static class CategoryErrors
{
    public static readonly Error AlreadyExists =
        Error.Conflict(
            "Category.AlreadyExists",
            "Já existe uma categoria com esse nome.");

    public static readonly Error NotFound =
        Error.NotFound(
            "Category.NotFound",
            "Categoria não encontrada.");

    public static readonly Error InvalidName =
        Error.Validation(
            "Category.InvalidName",
            "O nome da categoria é obrigatório.");
}
