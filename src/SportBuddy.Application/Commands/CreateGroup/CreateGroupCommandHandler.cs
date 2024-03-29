﻿using SportBuddy.Application.Abstractions;
using SportBuddy.Application.Exceptions;
using SportBuddy.Core.Entities;
using SportBuddy.Core.Repositories;

namespace SportBuddy.Application.Commands.CreateGroup;

internal sealed class CreateGroupCommandHandler(IGroupRepository groupRepository, IUserRepository userRepository) : ICommandHandler<CreateGroupCommand>
{
    public async Task Handle(CreateGroupCommand command, CancellationToken cancellationToken = default)
    {
        var (groupId, adminId, name, description, groupType) = command;
        
        var admin = await userRepository.GetByIdAsync(adminId);
        if (admin is null)
        {
            throw new UserNotFoundException(adminId);
        }
        
        var group = new Group(groupId, adminId, name, description, groupType);
        group.AddMember(admin);
        
        await groupRepository.AddAsync(group);
    }
}