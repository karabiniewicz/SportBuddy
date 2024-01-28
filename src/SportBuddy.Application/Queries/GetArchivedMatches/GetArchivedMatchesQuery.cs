using SportBuddy.Application.Abstractions;
using SportBuddy.Application.DTO;
using SportBuddy.Core.ValueObjects;

namespace SportBuddy.Application.Queries.GetArchivedMatches;

public sealed record GetArchivedMatchesQuery(GroupId GroupId) : IQuery<IEnumerable<MatchDto>>;