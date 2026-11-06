namespace ResumoCash.Domain.Common.Errors;

public static class CompetenceErrors
{
    public static readonly Error NotFound =
        Error.NotFound(
            "Competence.NotFound",
            "Competência não encontrada.");

    public static readonly Error AlreadyClosed =
        Error.Conflict(
            "Competence.AlreadyClosed",
            "A competência já está encerrada.");

    public static readonly Error AlreadyOpen =
        Error.Conflict(
            "Competence.AlreadyOpen",
            "O usuário já possui uma competência aberta.");

    public static readonly Error AlreadyExists =
        Error.Conflict(
            "Competence.AlreadyExists",
            "Já existe uma competência cadastrada para este mês.");
}
