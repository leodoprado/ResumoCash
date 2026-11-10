using System;
using System.Collections.Generic;
using System.Text;
using OneOf;
using ResumoCash.Domain.Common;
using ResumoCash.Domain.Common.Errors;
using ResumoCash.Domain.Repositories;

namespace ResumoCash.Application.Users.GetCurrent;

public class GetCurrentUserService
{
    private readonly IUserRepository _userRepository;

    public GetCurrentUserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<OneOf<GetCurrentUserResponse, AppError>> ExecuteAsync (Guid userId, CancellationToken cancellationToken)
    {
        var userResult = await _userRepository.GetByIdAsync(userId, cancellationToken);

        if (userResult is null)
            return UserErrors.NotFound;

        return new GetCurrentUserResponse(
            userResult.Id,
            userResult.Name,
            userResult.Email,
            userResult.CreatedAt
        );
    }
}
