using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SportBuddy.Application.DTO;
using SportBuddy.Application.Security;

namespace SportBuddy.Infrastructure.Auth;

internal sealed class Authenticator(IOptions<AuthOptions> options, TimeProvider timeProvider) : IAuthenticator
{
    private readonly string _issuer = options.Value.Issuer;
    private readonly TimeSpan _expiry = options.Value.Expiry ?? TimeSpan.FromHours(1);
    private readonly string _audience = options.Value.Audience;
    private readonly SigningCredentials _signingCredentials =
        new(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SigningKey)), SecurityAlgorithms.HmacSha256);
    private readonly JwtSecurityTokenHandler _jwtSecurityToken = new();

    public string CreateAccessToken(Guid userId, string role)
    {
        var now = timeProvider.GetLocalNow().DateTime;
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
            new(ClaimTypes.Role, role)
        };

        var expires = now.Add(_expiry);
        var jwt = new JwtSecurityToken(_issuer, _audience, claims, now, expires, _signingCredentials);
        var accessToken = _jwtSecurityToken.WriteToken(jwt);

        return accessToken;
    }
    
    public string CreateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}