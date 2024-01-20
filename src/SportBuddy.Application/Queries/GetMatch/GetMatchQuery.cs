using SportBuddy.Application.Abstractions;
using SportBuddy.Application.DTO;

namespace SportBuddy.Application.Queries.GetMatch;

public sealed record GetMatchQuery(Guid MatchId) : IQuery<MatchDto>;