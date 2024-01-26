using SportBuddy.Application.Abstractions;
using SportBuddy.Application.DTO;
using SportBuddy.Core.ValueObjects;

namespace SportBuddy.Application.Queries.GetUserMatches;

public sealed record GetUserMatchesQuery(UserId UserId, DateOnly? date) : IQuery<IEnumerable<MatchDto>>;