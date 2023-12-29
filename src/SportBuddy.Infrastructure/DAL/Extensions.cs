using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportBuddy.Core.Repositories;
using SportBuddy.Infrastructure.DAL.Repositories;

namespace SportBuddy.Infrastructure.DAL;

internal static class Extensions
{
    private const string OptionsSectionName = "postgres";
    
    public static IServiceCollection AddPostgres(this IServiceCollection services)//, IConfiguration configuration)
    {
        // TODO: move to appsettings 
        var connectionString = "Host=localhost;Database=SportBuddy;Username=postgres;Password=postgres";

        services.AddDbContext<SportBuddyDbContext>(x => x.UseNpgsql(connectionString));
        services.AddScoped<IGroupRepository, GroupRepository>();
        return services;
    }
}