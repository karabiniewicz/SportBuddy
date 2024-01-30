using Microsoft.EntityFrameworkCore;
using SportBuddy.Core.Entities;

namespace SportBuddy.Infrastructure.DAL;

internal sealed class SportBuddyDbContext(DbContextOptions<SportBuddyDbContext> options) : DbContext(options)
{
    public DbSet<Match> Matches { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}