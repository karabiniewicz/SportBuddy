using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportBuddy.Application;
using SportBuddy.Core.Repositories;
using SportBuddy.Infrastructure.DAL.Behaviors;
using SportBuddy.Infrastructure.DAL.Repositories;

namespace SportBuddy.Infrastructure.DAL;

internal static class Extensions
{
    private const string OptionsSectionName = "postgres";
    
    public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<PostgresOptions>(configuration.GetRequiredSection(OptionsSectionName));
        var postgresOptions = configuration.GetOptions<PostgresOptions>(OptionsSectionName);
        services.AddDbContext<SportBuddyDbContext>(x => x.UseNpgsql(postgresOptions.ConnectionString));
        
        services.AddScoped<IGroupRepository, GroupRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IMatchRepository, MatchRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddHostedService<DatabaseInitializer>();
        services.AddMediatR(c =>
        {
            c.RegisterServicesFromAssemblyContaining<ApplicationAssembly>();
            c.AddOpenBehavior(typeof(UnitOfWorkCommandHandlerBehavior<,>));
        });
        // Npgsql DateTime issue
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        return services;
    }
}