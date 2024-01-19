using SportBuddy.Application.Abstractions;
using SportBuddy.Application.DTO;
using SportBuddy.Application.Exceptions;
using SportBuddy.Core.Repositories;

namespace SportBuddy.Application.Queries.GetGroupMembers;

internal sealed class GetGroupMembersQueryHandler(IGroupRepository groupRepository): IQueryHandler<GetGroupMembersQuery, IEnumerable<UserDto>>
{
    public async Task<IEnumerable<UserDto>> HandleAsync(GetGroupMembersQuery query)
    {
        var group = await groupRepository.GetAsync(query.GroupId) ?? throw new GroupNotFoundException(query.GroupId);
        return group.Members
            .Select(x => x.AsDto());
    }
}