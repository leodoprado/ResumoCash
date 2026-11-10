namespace ResumoCash.Domain.Common.Errors;

public static class UserErrors
{
    public static readonly AppError EmailAlreadyExists =
        AppError.Conflict(
            "User.EmailAlreadyExists",
            "Já existe um usuário cadastrado com esse e-mail.");

    public static readonly AppError NotFound =
        AppError.NotFound(
            "User.NotFound",
            "Usuário não encontrado.");

    public static readonly AppError InvalidName =
        AppError.Validation(
            "User.InvalidName",
            "O nome do usuário é obrigatório.");

    public static readonly AppError InvalidEmail =
        AppError.Validation(
            "User.InvalidEmail",
            "O e-mail informado é inválido.");

    public static readonly AppError InvalidPassword =
        AppError.Validation(
            "User.InvalidPassword",
            "A senha informada não atende aos requisitos de segurança.");

    public static readonly AppError InvalidCredentials =
        AppError.Validation(
            "User.InvalidCredentials",
            "E-mail ou senha inválidos.");
}
