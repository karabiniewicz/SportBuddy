using SportBuddy.Application.Abstractions;
using SportBuddy.Application.DTO;
using SportBuddy.Application.Exceptions;
using SportBuddy.Core.Repositories;

namespace SportBuddy.Application.Queries.GetMatchMembers;

internal sealed class GetMatchMembersQueryHandler(IMatchRepository matchRepository) : IQueryHandler<GetMatchMembersQuery, IEnumerable<UserDto>>
{
    public async Task<IEnumerable<UserDto>> Handle(GetMatchMembersQuery query, CancellationToken cancellationToken = default)
    {
        var match = await matchRepository.GetByIdAsync(query.MatchId) ?? throw new MatchNotFoundException(query.MatchId);
        return match.Members
            .Select(x => x.AsDto());
    }
}