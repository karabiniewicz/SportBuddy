using SportBuddy.Core.ValueObjects;

namespace SportBuddy.Core.Entities;

public class User(UserId id, Email email, Username username, Password password, FullName fullName, Role role, DateTimeOffset createdAt)
{
    public UserId Id { get; private set; } = id;
    public Email Email { get; private set; } = email;
    public Username Username { get; private set; } = username;
    public Password Password { get; private set; } = password;
    public FullName FullName { get; private set; } = fullName;
    public Role Role { get; private set; } = role;
    public DateTimeOffset CreatedAt { get; private set; } = createdAt;
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    
    public void SetUserRefreshToken(string refreshToken, DateTime expiryTime)
    {
        RefreshToken = refreshToken;
        RefreshTokenExpiryTime = expiryTime;
    }
}
