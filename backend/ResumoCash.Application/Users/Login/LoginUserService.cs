using OneOf;
using ResumoCash.Domain.Common;
using ResumoCash.Domain.Common.Errors;
using ResumoCash.Domain.Enums;
using ResumoCash.Domain.Repositories;
using ResumoCash.Domain.Security;

namespace ResumoCash.Application.Users.Login;

public sealed class LoginUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;

    public LoginUserService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        ITokenService tokenService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
    }

    public async Task<OneOf<LoginUserResponse, AppError>> ExecuteAsync(
        LoginUserRequest request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            return UserErrors.InvalidCredentials;

        var email = request.Email.Trim().ToLowerInvariant();

        var user = await _userRepository.GetByEmailAsync(
            email,
            cancellationToken);

        if (user is null)
            return UserErrors.InvalidCredentials;

        var validPassword = _passwordHasher.Verify(
            request.Password,
            user.PasswordHash);

        if (!validPassword)
            return UserErrors.InvalidCredentials;

        if (user.Status != UserStatus.Active)
            return UserErrors.InvalidCredentials;

        var accessToken = _tokenService.GenerateToken(user);

        return new LoginUserResponse(
            AccessToken: accessToken,
            TokenType: "Bearer",
            ExpiresIn: 3600
        );
    }
}
