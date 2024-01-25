using SportBuddy.Application.Abstractions;
using SportBuddy.Application.DTO;
using SportBuddy.Core.ValueObjects;

namespace SportBuddy.Application.Queries.GetGroupUsersToInvite;

 public sealed record GetGroupUsersToInviteQuery(GroupId GroupId, UserId UserId) : IQuery<IEnumerable<UserDto>>;
