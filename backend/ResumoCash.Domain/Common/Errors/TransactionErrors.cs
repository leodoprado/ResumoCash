namespace ResumoCash.Domain.Common.Errors;

public static class TransactionErrors
{
    public static readonly AppError NotFound =
        AppError.NotFound(
            "Transaction.NotFound",
            "Transação não encontrada.");

    public static readonly AppError InvalidAmount =
        AppError.Validation(
            "Transaction.InvalidAmount",
            "O valor da transação deve ser maior que zero.");

    public static readonly AppError InvalidDate =
        AppError.Validation(
            "Transaction.InvalidDate",
            "A data da transação é inválida.");

    public static readonly AppError CompetenceClosed =
        AppError.Conflict(
            "Transaction.CompetenceClosed",
            "Não é possível realizar transações em uma competência encerrada.");

    public static readonly AppError CategoryNotFound =
        AppError.NotFound(
            "Transaction.CategoryNotFound",
            "A categoria informada não foi encontrada.");

    public static readonly AppError CompetenceNotFound =
        AppError.NotFound(
            "Transaction.CompetenceNotFound",
            "A competência informada não foi encontrada.");
}
