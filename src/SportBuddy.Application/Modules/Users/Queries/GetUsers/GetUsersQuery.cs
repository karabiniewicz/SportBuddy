using SportBuddy.Application.Abstractions;
using SportBuddy.Application.DTO;

namespace SportBuddy.Application.Modules.Users.Queries.GetUsers;

public class GetUsersQuery: IQuery<IEnumerable<UserDto>>
{
}