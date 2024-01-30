using SportBuddy.Application.Abstractions;
using SportBuddy.Application.Exceptions;
using SportBuddy.Core.Repositories;

namespace SportBuddy.Application.Commands.LeaveGroup;

internal sealed class LeaveGroupCommandHandler(IGroupRepository groupRepository) : ICommandHandler<LeaveGroupCommand>
{
    public async Task Handle(LeaveGroupCommand command, CancellationToken cancellationToken = default)
    {
        var (groupId, userId) = command;

        var group = await groupRepository.GetAsync(groupId);

        if (group is null)
        {
            throw new GroupNotFoundException(groupId);
        }

        if (group.Members.All(m => m.Id != userId))
        {
            throw new UserIsNotMemberOfGroupException(userId, groupId);
        }

        group.RemoveMember(userId);

        await groupRepository.UpdateAsync(group);
    }
}