using SportBuddy.Application.DTO;

namespace SportBuddy.Application.Security;

public interface IAuthenticator
{
    JwtDto CreateToken(Guid userId, string role);
}