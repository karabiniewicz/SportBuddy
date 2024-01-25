using SportBuddy.Application.Abstractions;
using SportBuddy.Application.DTO;
using SportBuddy.Application.Exceptions;
using SportBuddy.Core.Repositories;

namespace SportBuddy.Application.Queries.GetGroupUsersToInvite;

internal sealed class GetGroupUsersToInviteQueryHandler(IGroupRepository groupRepository, IUserRepository userRepository) : IQueryHandler<GetGroupUsersToInviteQuery, IEnumerable<UserDto>>
{
    public async Task<IEnumerable<UserDto>> HandleAsync(GetGroupUsersToInviteQuery query)
    {
        var group = await groupRepository.GetAsync(query.GroupId) ?? throw new GroupNotFoundException(query.GroupId);
        if (group.AdminId != query.UserId)
        {
            throw new UserIsNotAdminException(query.UserId, query.GroupId);
        }
        
        var groupMembers = group.Members.Select(m => m.Id);
        var users = await userRepository.GetUsersToInviteAsync(groupMembers);
        return users.Select(x => x.AsDto());
    }
}