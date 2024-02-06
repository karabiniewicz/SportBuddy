using SportBuddy.Application.DTO;

namespace SportBuddy.Application.Security;

public interface ITokenStorage
{
    void Set(JwtDto jwt);
    JwtDto Get();
    void SetRefreshTokenCookie(RefreshTokenDto refreshToken);
}