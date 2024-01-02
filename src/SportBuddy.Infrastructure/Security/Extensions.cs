using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SportBuddy.Application.Security;
using SportBuddy.Core.Entities;

namespace SportBuddy.Infrastructure.Security;

internal static class Extensions
{
    public static IServiceCollection AddSecurity(this IServiceCollection services)
    {
        services
            .AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>()
            .AddSingleton<IPasswordManager, PasswordManager>();

        return services;
    }
}