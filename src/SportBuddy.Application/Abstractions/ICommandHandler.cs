using MediatR;

namespace SportBuddy.Application.Abstractions;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand> where TCommand : class, ICommand
{
    Task Handle(TCommand command, CancellationToken cancellationToken = default);
}