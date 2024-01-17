using SportBuddy.Application.Abstractions;
using SportBuddy.Application.DTO;
using SportBuddy.Core.Repositories;

namespace SportBuddy.Application.Queries.GetUsers;

internal sealed class GetUsersQueryHandler(IUserRepository userRepository): IQueryHandler<GetUsersQuery, IEnumerable<UserDto>>
{
    public async Task<IEnumerable<UserDto>> HandleAsync(GetUsersQuery query)
    {
         var users = userRepository.GetAll();
          return users
              .Select(x => x.AsDto());
    }
}