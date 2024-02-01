using MediatR;
using SportBuddy.Application.Abstractions;

namespace SportBuddy.Infrastructure.DAL.Behaviors;

public sealed class UnitOfWorkCommandHandlerBehavior<TRequest, TResponse>(IUnitOfWork unitOfWork) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request is not ICommand)
        {
            return await next();
        }

        await using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
        
        try
        {
            var response = await next();
            await unitOfWork.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            return response;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}