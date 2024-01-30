using MediatR;

namespace SportBuddy.Application.Abstractions;

public interface IQuery<TResult> : IRequest<TResult>
{
    
}