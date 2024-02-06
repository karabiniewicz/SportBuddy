namespace SportBuddy.Infrastructure.Auth;

public sealed class AuthOptions
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string SigningKey { get; set; }
    public TimeSpan? AccessTokenExpiry { get; set; }
    public TimeSpan? RefreshTokenExpiry { get; set; }
}