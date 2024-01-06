using SportBuddy.Application.Abstractions;
using SportBuddy.Application.DTO;

namespace SportBuddy.Application.Queries.GetUser;

public sealed record GetUserQuery(Guid UserId): IQuery<UserDto>;