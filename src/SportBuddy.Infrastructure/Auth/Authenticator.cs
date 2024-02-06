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
    private readonly TimeSpan _accessTokenExpiry = options.Value.AccessTokenExpiry ?? TimeSpan.FromHours(1);
    private readonly TimeSpan _refreshTokenExpiry = options.Value.RefreshTokenExpiry ?? TimeSpan.FromDays(1);
    private readonly string _audience = options.Value.Audience;
    private readonly SigningCredentials _signingCredentials =
        new(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SigningKey)), SecurityAlgorithms.HmacSha256);
    private readonly JwtSecurityTokenHandler _jwtSecurityToken = new();
    private readonly DateTime _now = timeProvider.GetLocalNow().DateTime;

    public JwtDto CreateAccessToken(Guid userId, string role)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
            new(ClaimTypes.Role, role)
        };

        var expires = _now.Add(_accessTokenExpiry);
        var jwt = new JwtSecurityToken(_issuer, _audience, claims, _now, expires, _signingCredentials);
        var accessToken = _jwtSecurityToken.WriteToken(jwt);

        return new JwtDto(accessToken);
    }
    
    public RefreshTokenDto CreateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        var token = Convert.ToBase64String(randomNumber);
        var expires = _now.Add(_refreshTokenExpiry);
        return new RefreshTokenDto(token, expires);
    }
}