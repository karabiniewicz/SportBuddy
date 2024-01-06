using SportBuddy.Application.Abstractions;
using SportBuddy.Application.DTO;

namespace SportBuddy.Application.Queries.GetUsers;

public sealed record GetUsersQuery : IQuery<IEnumerable<UserDto>>;