using Microsoft.AspNetCore.Http;
using SportBuddy.Application.DTO;
using SportBuddy.Application.Security;

namespace SportBuddy.Infrastructure.Auth;

internal sealed class HttpContextTokenStorage(IHttpContextAccessor httpContextAccessor) : ITokenStorage
{
    private const string JwtKey = "jwt";
    private const string RefreshTokenKey = "refresh_token";

    public void Set(JwtDto jwt)
        => httpContextAccessor.HttpContext?.Items.TryAdd(JwtKey, jwt);

    public JwtDto Get()
    {
        if (httpContextAccessor.HttpContext is null)
        {
            return null;
        }

        if (httpContextAccessor.HttpContext.Items.TryGetValue(JwtKey, out var jwt))
        {
            return jwt as JwtDto;
        }

        return null;
    }

    public void SetRefreshTokenCookie(RefreshTokenDto refreshToken)
    {
        httpContextAccessor?.HttpContext?.Response.Cookies.Delete(RefreshTokenKey);
        httpContextAccessor?.HttpContext?.Response.Cookies.Append(RefreshTokenKey, refreshToken.Token,
            new CookieOptions
            {
                HttpOnly = true, 
                Secure = true, 
                SameSite = SameSiteMode.Strict, 
                Expires = refreshToken.ExpiryTime, 
                Path = "/users/refresh"
            });
    }
}