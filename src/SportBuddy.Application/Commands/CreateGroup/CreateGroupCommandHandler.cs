using SportBuddy.Application.Abstractions;
using SportBuddy.Application.Exceptions;
using SportBuddy.Core.Entities;
using SportBuddy.Core.Repositories;

namespace SportBuddy.Application.Commands.CreateGroup;

internal sealed class CreateGroupCommandHandler(IGroupRepository groupRepository) : ICommandHandler<CreateGroupCommand>
{
    public async Task HandleAsync(CreateGroupCommand command)
    {
        var (groupId, adminId, name, description, groupType) = command;
        
        if (await groupRepository.GetByNameAsync(name) is not null)
        {
            throw new GroupNameAlreadyInUseException(name);
        }
        
        var group = new Group(groupId, adminId, name, description, groupType);
        
        await groupRepository.AddAsync(group);
    }
}