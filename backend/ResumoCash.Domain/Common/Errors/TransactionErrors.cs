namespace ResumoCash.Domain.Common.Errors;

public static class TransactionErrors
{
    public static readonly Error NotFound =
        Error.NotFound(
            "Transaction.NotFound",
            "Transação não encontrada.");

    public static readonly Error InvalidAmount =
        Error.Validation(
            "Transaction.InvalidAmount",
            "O valor da transação deve ser maior que zero.");

    public static readonly Error InvalidDate =
        Error.Validation(
            "Transaction.InvalidDate",
            "A data da transação é inválida.");

    public static readonly Error CompetenceClosed =
        Error.Conflict(
            "Transaction.CompetenceClosed",
            "Não é possível realizar transações em uma competência encerrada.");

    public static readonly Error CategoryNotFound =
        Error.NotFound(
            "Transaction.CategoryNotFound",
            "A categoria informada não foi encontrada.");

    public static readonly Error CompetenceNotFound =
        Error.NotFound(
            "Transaction.CompetenceNotFound",
            "A competência informada não foi encontrada.");
}
