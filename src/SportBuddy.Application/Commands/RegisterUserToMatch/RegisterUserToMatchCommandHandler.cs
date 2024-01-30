using SportBuddy.Application.Abstractions;
using SportBuddy.Application.Exceptions;
using SportBuddy.Core.Repositories;

namespace SportBuddy.Application.Commands.RegisterUserToMatch;

internal sealed class RegisterUserToMatchCommandHandler(IMatchRepository matchRepository, IUserRepository userRepository) : ICommandHandler<RegisterUserToMatchCommand>
{
    public async Task Handle(RegisterUserToMatchCommand command, CancellationToken cancellationToken = default)
    {
        var (matchId, userId) = command;
        
        var match = await matchRepository.GetByIdAsync(matchId);
        
        if (match is null)
        {
            throw new MatchNotFoundException(matchId);
        }

        var user = await userRepository.GetByIdAsync(userId);
        
        if (user is null)
        {
            throw new UserNotFoundException(userId);
        }
        
        if (match.Members.Any(p => p.Id == userId))
        {
            throw new UserAlreadyParticipantOfMatchException(userId, matchId);
        }
        
        match.AddMember(user);
        await matchRepository.UpdateAsync(match);
    }
}