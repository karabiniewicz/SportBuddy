using Microsoft.EntityFrameworkCore;
using SportBuddy.Core.Entities;

namespace SportBuddy.Infrastructure.DAL;

internal sealed class SportBuddyDbContext: DbContext
{
    public DbSet<Match> Matches { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<User> Users { get; set; }

    public SportBuddyDbContext(DbContextOptions<SportBuddyDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}