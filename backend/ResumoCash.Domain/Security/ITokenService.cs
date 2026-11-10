using ResumoCash.Domain.Entities;

namespace ResumoCash.Domain.Security;

public interface ITokenService
{
    string GenerateToken(User user);
}
