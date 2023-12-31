using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportBuddy.Infrastructure.DAL;

namespace SportBuddy.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddPostgres(configuration);
        // TODO

        return services;
    }
    
    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
    {
        var options = new T();
        var section = configuration.GetRequiredSection(sectionName);
        section.Bind(options);

        return options;
    }
}