using SportBuddy.Application.Abstractions;

namespace SportBuddy.Infrastructure.DAL.Decorators;

internal sealed class UnitOfWorkCommandHandlerDecorator<TCommand>(ICommandHandler<TCommand> commandHandler, IUnitOfWork unitOfWork)
    : ICommandHandler<TCommand> where TCommand : class, ICommand
{
    public async Task Handle(TCommand command, CancellationToken cancellationToken = default) 
        => await unitOfWork.ExecuteAsync(() => commandHandler.Handle(command, cancellationToken));
}