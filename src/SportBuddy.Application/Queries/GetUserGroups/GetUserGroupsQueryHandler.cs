using SportBuddy.Application.Abstractions;
using SportBuddy.Application.DTO;
using SportBuddy.Core.Repositories;

namespace SportBuddy.Application.Queries.GetUserGroups;

internal sealed class GetUserGroupsQueryHandler(IGroupRepository groupRepository) : IQueryHandler<GetUserGroupsQuery, IEnumerable<GroupDto>>
{
    public async Task<IEnumerable<GroupDto>> Handle(GetUserGroupsQuery query, CancellationToken cancellationToken = default)
    {
        var groups = await groupRepository.GetAllByUserIdAsync(query.UserId);
        return groups.Select(x => x.AsDto());
    }
}