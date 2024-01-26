using SportBuddy.Application.Abstractions;
using SportBuddy.Application.DTO;
using SportBuddy.Core.Repositories;

namespace SportBuddy.Application.Queries.GetUserMatches;

internal sealed class GetUserMatchesQueryHandler(IMatchRepository matchRepository, TimeProvider timeProvider) : IQueryHandler<GetUserMatchesQuery, IEnumerable<MatchDto>>
{
    public async Task<IEnumerable<MatchDto>> HandleAsync(GetUserMatchesQuery query)
    {
        var (userId, date) = query;
        date ??= DateOnly.FromDateTime(timeProvider.GetLocalNow().DateTime);
        
        var matches = await matchRepository.GetByUserIdAndDateAsync(userId, date.Value);
        return matches.Select(x => x.AsDto());
    }
}