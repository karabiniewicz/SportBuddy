using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportBuddy.Core.Consts;
using SportBuddy.Core.Entities;

namespace SportBuddy.Infrastructure.DAL;

internal sealed class DatabaseInitializer(IServiceProvider serviceProvider) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<SportBuddyDbContext>();
        await dbContext.Database.MigrateAsync(cancellationToken);

        if (await dbContext.Groups.AnyAsync(cancellationToken))
        {
            return;
        }
        
        var groups = new List<Group>
        {
            new ("G1", "group 1", GroupType.Public),
            new ("G2", "group 2 private", GroupType.Private),
            new ("G3", "group 3", GroupType.Public),
            new ("G4", "group 4", GroupType.Public),
            new ("G5", "group 5", GroupType.Public)
        };
        
        await dbContext.Groups.AddRangeAsync(groups, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}