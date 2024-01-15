﻿using SportBuddy.Application.Abstractions;
using SportBuddy.Core.Entities;
using SportBuddy.Core.Repositories;

namespace SportBuddy.Application.Commands.CreateGroup;

internal sealed class CreateGroupCommandHandler(IGroupRepository groupRepository) : ICommandHandler<CreateGroupCommand>
{
    public async Task HandleAsync(CreateGroupCommand command)
    {
        var (groupId, adminId, name, description, groupType) = command;
        
        // TODO: check if user have group with the same name
        
        // TODO: add adminId to group 
        
        var group = new Group(groupId, name, description, groupType);
        
        await groupRepository.AddAsync(group);
    }
}