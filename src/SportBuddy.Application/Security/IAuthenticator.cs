using SportBuddy.Core.Entities;

namespace SportBuddy.Application.Security;

public interface IAuthenticator
{
    void Authenticate(User user);
}