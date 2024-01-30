using SportBuddy.Application.Abstractions;
using SportBuddy.Application.DTO;
using SportBuddy.Application.Exceptions;
using SportBuddy.Core.Repositories;

namespace SportBuddy.Application.Queries.GetGroup;

internal sealed class GetGroupQueryHandler(IGroupRepository groupRepository): IQueryHandler<GetGroupQuery, GroupDto>
{
    public async Task<GroupDto> Handle(GetGroupQuery query, CancellationToken cancellationToken = default)
    {
        var group = await groupRepository.GetAsync(query.GroupId) ?? throw new GroupNotFoundException(query.GroupId);
        return group.AsDto();
    }
}
