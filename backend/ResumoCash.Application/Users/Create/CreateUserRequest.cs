namespace ResumoCash.Application.Users.Create;

public record CreateUserRequest(
    string Name,
    string Email,
    string Password
);
