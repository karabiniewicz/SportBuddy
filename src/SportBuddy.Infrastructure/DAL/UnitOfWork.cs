using Microsoft.EntityFrameworkCore.Storage;

namespace SportBuddy.Infrastructure.DAL;

internal sealed class UnitOfWork(SportBuddyDbContext dbContext) : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        => await dbContext.SaveChangesAsync(cancellationToken);

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken) 
        => await dbContext.Database.BeginTransactionAsync(cancellationToken);
}