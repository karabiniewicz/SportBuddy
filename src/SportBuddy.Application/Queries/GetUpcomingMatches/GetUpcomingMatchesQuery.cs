using SportBuddy.Application.Abstractions;
using SportBuddy.Application.DTO;
using SportBuddy.Core.ValueObjects;

namespace SportBuddy.Application.Queries.GetUpcomingMatches;

public sealed record GetUpcomingMatchesQuery(GroupId GroupId) : IQuery<IEnumerable<MatchDto>>;