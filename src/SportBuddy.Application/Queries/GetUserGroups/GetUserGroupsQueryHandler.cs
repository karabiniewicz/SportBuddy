using SportBuddy.Application.Abstractions;
using SportBuddy.Application.DTO;
using SportBuddy.Core.Repositories;

namespace SportBuddy.Application.Queries.GetUserGroups;

internal sealed class GetUserGroupsQueryHandler(IGroupRepository groupRepository) : IQueryHandler<GetUserGroupsQuery, IEnumerable<GroupDto>>
{
    public async Task<IEnumerable<GroupDto>> HandleAsync(GetUserGroupsQuery query)
    {
        var groups = await groupRepository.GetAllByUserIdAsync(query.UserId);
        return groups.Select(x => x.AsDto());
    }
}