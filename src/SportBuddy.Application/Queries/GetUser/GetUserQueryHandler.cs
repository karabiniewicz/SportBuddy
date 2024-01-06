using SportBuddy.Application.Abstractions;
using SportBuddy.Application.DTO;
using SportBuddy.Core.Repositories;

namespace SportBuddy.Application.Queries.GetUser;

internal sealed class GetUserQueryHandler(IUserRepository userRepository): IQueryHandler<GetUserQuery, UserDto>
{
    public async Task<UserDto> HandleAsync(GetUserQuery query)
    {
        var user = await userRepository.GetByIdAsync(query.UserId);
        return user?.AsDto();
    }
}