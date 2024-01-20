using SportBuddy.Application.Abstractions;
using SportBuddy.Application.Commands;
using SportBuddy.Application.DTO;
using SportBuddy.Application.Exceptions;
using SportBuddy.Core.Repositories;

namespace SportBuddy.Application.Queries.GetMatch;

internal sealed class GetMatchQueryHandler(IMatchRepository matchRepository): IQueryHandler<GetMatchQuery, MatchDto>
{
    public async Task<MatchDto> HandleAsync(GetMatchQuery query)
    {
        var match = await matchRepository.GetAsync(query.MatchId) ?? throw new MatchNotFoundException(query.MatchId);
        return match.AsDto();
    }
}
    