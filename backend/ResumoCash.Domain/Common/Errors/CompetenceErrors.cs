namespace ResumoCash.Domain.Common.Errors;

public static class CompetenceErrors
{
    public static readonly AppError NotFound =
        AppError.NotFound(
            "Competence.NotFound",
            "Competência não encontrada.");

    public static readonly AppError AlreadyClosed =
        AppError.Conflict(
            "Competence.AlreadyClosed",
            "A competência já está encerrada.");

    public static readonly AppError AlreadyOpen =
        AppError.Conflict(
            "Competence.AlreadyOpen",
            "O usuário já possui uma competência aberta.");

    public static readonly AppError AlreadyExists =
        AppError.Conflict(
            "Competence.AlreadyExists",
            "Já existe uma competência cadastrada para este mês.");
}
