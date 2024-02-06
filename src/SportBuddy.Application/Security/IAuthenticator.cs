using SportBuddy.Application.DTO;

namespace SportBuddy.Application.Security;

public interface IAuthenticator
{
    JwtDto CreateAccessToken(Guid userId, string role);
    RefreshTokenDto CreateRefreshToken();
}