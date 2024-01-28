using SportBuddy.Application.Abstractions;
using SportBuddy.Application.DTO;
using SportBuddy.Application.Exceptions;
using SportBuddy.Core.Repositories;

namespace SportBuddy.Application.Queries.GetArchivedMatches;

internal sealed class GetArchivedMatchesQueryHandler(IGroupRepository groupRepository, IMatchRepository matchRepository, TimeProvider timeProvider) 
    : IQueryHandler<GetArchivedMatchesQuery, IEnumerable<MatchDto>>
{
    public async Task<IEnumerable<MatchDto>> HandleAsync(GetArchivedMatchesQuery query)
    {
        _ = await groupRepository.GetAsync(query.GroupId) ?? throw new GroupNotFoundException(query.GroupId);
        
        var today = DateOnly.FromDateTime(timeProvider.GetLocalNow().DateTime);
        
        var matches = await matchRepository.GetArchivedMatchesAsync(query.GroupId, today);
        
        return matches.Select(x => x.AsDto());
    }
}