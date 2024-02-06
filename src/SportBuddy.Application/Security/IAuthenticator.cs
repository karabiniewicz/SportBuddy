using SportBuddy.Application.DTO;

namespace SportBuddy.Application.Security;

public interface IAuthenticator
{
    string CreateAccessToken(Guid userId, string role);
    string CreateRefreshToken();
}