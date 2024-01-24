using SportBuddy.Application.Abstractions;
using SportBuddy.Application.DTO;
using SportBuddy.Core.ValueObjects;

namespace SportBuddy.Application.Queries.GetUserGroups;

public sealed record GetUserGroupsQuery(UserId UserId) : IQuery<IEnumerable<GroupDto>>;