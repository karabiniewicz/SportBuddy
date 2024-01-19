using SportBuddy.Application.Abstractions;
using SportBuddy.Application.DTO;

namespace SportBuddy.Application.Queries.GetGroupMembers;

public sealed record GetGroupMembersQuery(Guid GroupId) : IQuery<IEnumerable<UserDto>>;