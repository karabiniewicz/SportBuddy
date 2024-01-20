using SportBuddy.Application.Abstractions;
using SportBuddy.Application.DTO;

namespace SportBuddy.Application.Queries.GetMatchMembers;

public sealed record GetMatchMembersQuery(Guid MatchId) : IQuery<IEnumerable<UserDto>>;