using OneOf;
using ResumoCash.Domain.Common;
using ResumoCash.Domain.Common.Errors;
using ResumoCash.Domain.Entities;
using ResumoCash.Domain.Repositories;
using ResumoCash.Domain.Security;

namespace ResumoCash.Application.Users.Create;

public class CreateUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public CreateUserService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<OneOf<CreateUserResponse, AppError>> ExecuteAsync(
        CreateUserRequest request,
        CancellationToken cancellationToken)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        var existsByEmail = await _userRepository.ExistsByEmailAsync(
            email,
            cancellationToken
        );

        if (existsByEmail)
            return UserErrors.EmailAlreadyExists;

        var passwordHash = _passwordHasher.Hash(request.Password);

        var user = new User(
            request.Name,
            email,
            passwordHash    
        );

        await _userRepository.AddAsync(user, cancellationToken);

        await _userRepository.SaveChangesAsync(cancellationToken);

        return new CreateUserResponse(
            user.Id,
            user.Name,
            user.Email
        );
    }
}
