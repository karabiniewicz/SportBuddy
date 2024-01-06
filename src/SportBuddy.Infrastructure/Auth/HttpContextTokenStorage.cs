using Microsoft.AspNetCore.Http;
using SportBuddy.Application.DTO;
using SportBuddy.Application.Security;

namespace SportBuddy.Infrastructure.Auth;

internal sealed class HttpContextTokenStorage(IHttpContextAccessor httpContextAccessor) : ITokenStorage
{
    private const string TokenKey = "jwt";

    public void Set(JwtDto jwt) 
        => httpContextAccessor.HttpContext?.Items.TryAdd(TokenKey, jwt);

    public JwtDto Get()
    {
        if (httpContextAccessor.HttpContext is null)
        {
            return null;
        }

        if (httpContextAccessor.HttpContext.Items.TryGetValue(TokenKey, out var jwt))
        {
            return jwt as JwtDto;
        }

        return null;
    }
}