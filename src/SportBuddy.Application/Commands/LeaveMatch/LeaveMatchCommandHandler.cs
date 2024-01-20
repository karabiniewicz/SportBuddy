using SportBuddy.Application.Abstractions;
using SportBuddy.Application.Exceptions;
using SportBuddy.Core.Repositories;

namespace SportBuddy.Application.Commands.LeaveMatch;

internal sealed class LeaveMatchCommandHandler(IMatchRepository matchRepository) : ICommandHandler<LeaveMatchCommand>
{
    public async Task HandleAsync(LeaveMatchCommand command)
    {
        var (matchId, userId) = command;

        var match = await matchRepository.GetAsync(matchId);

        if (match is null)
        {
            throw new MatchNotFoundException(matchId);
        }

        if (match.Members.All(m => m.Id != userId))
        {
            throw new UserIsNotMemberOfMatchException(userId, matchId);
        }

        match.RemoveMember(userId);

        await matchRepository.UpdateAsync(match);
    }
}