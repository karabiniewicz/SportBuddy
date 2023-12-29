using Microsoft.Extensions.DependencyInjection;
using SportBuddy.Infrastructure.DAL;

namespace SportBuddy.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services
            .AddPostgres();
        // TODO

        return services;
    }
}