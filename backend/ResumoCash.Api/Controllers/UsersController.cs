using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResumoCash.Api.Common.Extensions;
using ResumoCash.Application.Users.Create;
using ResumoCash.Application.Users.GetCurrent;
using ResumoCash.Application.Users.Login;
using ResumoCash.Domain.Entities;

namespace ResumoCash.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly CreateUserService _createUserService;
    private readonly LoginUserService _loginUserService;
    private readonly GetCurrentUserService _getCurrentUserService;

    public UsersController(CreateUserService createUserService, LoginUserService loginUserService, GetCurrentUserService getCurrentUserService)
    {
        _createUserService = createUserService;
        _loginUserService = loginUserService;
        _getCurrentUserService = getCurrentUserService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
    {
        var userResult = await _createUserService.ExecuteAsync(request, cancellationToken);

        if (userResult.IsError())
            return userResult.GetErrorResult().ToActionResult(this);

        var user = userResult.GetSuccessResult();

        return Created($"/api/users/{user.Id}", user);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserRequest request, CancellationToken cancellationToken)
    {
        var userResult = await _loginUserService.ExecuteAsync(request, cancellationToken);

        if (userResult.IsError())
            return userResult.GetErrorResult().ToActionResult(this);

        var user = userResult.GetSuccessResult();

        return Ok(user);
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser(CancellationToken cancellationToken)
    {
        var userIdClaim = User.FindFirstValue(JwtRegisteredClaimNames.Sub);

        if (!Guid.TryParse(userIdClaim, out var userId))
            return Unauthorized();

        var userResult = await _getCurrentUserService.ExecuteAsync(userId, cancellationToken);

        if (userResult.IsError())
            return userResult.GetErrorResult().ToActionResult(this);

        var user = userResult.GetSuccessResult();

        return Ok(user);
    }
}
