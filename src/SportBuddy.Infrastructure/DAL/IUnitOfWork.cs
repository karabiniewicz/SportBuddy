using Microsoft.EntityFrameworkCore.Storage;

namespace SportBuddy.Infrastructure.DAL;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken);
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);

}