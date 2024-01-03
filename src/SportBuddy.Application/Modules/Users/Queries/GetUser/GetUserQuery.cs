using SportBuddy.Application.Abstractions;
using SportBuddy.Application.DTO;

namespace SportBuddy.Application.Modules.Users.Queries.GetUser;

public class GetUserQuery: IQuery<UserDto>
{
    public Guid UserId { get; set; }
}

public class GetUsersQuery : IQuery<IEnumerable<UserDto>>
{
}