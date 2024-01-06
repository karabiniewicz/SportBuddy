using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportBuddy.Infrastructure.Auth;
using SportBuddy.Infrastructure.DAL;
using SportBuddy.Infrastructure.Exceptions;
using SportBuddy.Infrastructure.Security;

namespace SportBuddy.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddSingleton<ExceptionMiddleware>();
        services.AddSecurity();
        services.AddAuth(configuration);
        services.AddHttpContextAccessor();

        services
            .AddPostgres(configuration)
            .AddSingleton(TimeProvider.System);
        
        // TODO swagger, security
        
        return services;
    }
    
    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseAuthentication();
        app.UseAuthorization();
        // TODO: swagger
        app.MapControllers();
        
        return app;
    }
    
    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
    {
        var options = new T();
        var section = configuration.GetRequiredSection(sectionName);
        section.Bind(options);

        return options;
    }
}