using SportBuddy.Application.Abstractions;
using SportBuddy.Application.Exceptions;
using SportBuddy.Core.Repositories;
using SportBuddy.Core.ValueObjects;

namespace SportBuddy.Application.Commands.AddGroupMembers;

internal sealed class AddGroupMembersCommandHandler(IGroupRepository groupRepository, IUserRepository userRepository) : ICommandHandler<AddGroupMembersCommand>
{
    public async Task Handle(AddGroupMembersCommand command, CancellationToken cancellationToken = default)
    {
        var (groupId, userIds) = command;

        var group = await groupRepository.GetAsync(groupId);

        if (group is null)
        {
            throw new GroupNotFoundException(groupId);
        }

        foreach (UserId userId in userIds)
        {
            var user = await userRepository.GetByIdAsync(userId);
            if (user is null)
            {
                throw new UserNotFoundException(userId);
            }
            
            if (group.Members.Any(m => m.Id == userId))
            {
                throw new UserAlreadyMemberOfGroupException(userId, groupId);
            }
            group.AddMember(user);
        }

        await groupRepository.UpdateAsync(group);
    }
}