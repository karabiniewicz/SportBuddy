using SportBuddy.Application.Abstractions;
using SportBuddy.Application.DTO;
using SportBuddy.Application.Exceptions;
using SportBuddy.Core.Repositories;

namespace SportBuddy.Application.Queries.GetUpcomingMatches;

internal sealed class GetUpcomingMatchesQueryHandler(IGroupRepository groupRepository, IMatchRepository matchRepository, TimeProvider timeProvider) 
    : IQueryHandler<GetUpcomingMatchesQuery, IEnumerable<MatchDto>>
{
    public async Task<IEnumerable<MatchDto>> HandleAsync(GetUpcomingMatchesQuery query)
    {
        _ = await groupRepository.GetAsync(query.GroupId) ?? throw new GroupNotFoundException(query.GroupId);
        
        var today = DateOnly.FromDateTime(timeProvider.GetLocalNow().DateTime);
        
        var matches = await matchRepository.GetUpcomingMatchesAsync(query.GroupId, today);
        
        return matches.Select(x => x.AsDto());
    }
}
