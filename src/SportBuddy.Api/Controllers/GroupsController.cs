﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportBuddy.Application.Abstractions;
using SportBuddy.Application.Commands.CreateGroup;
using SportBuddy.Core.Entities;
using SportBuddy.Core.Repositories;
using Swashbuckle.AspNetCore.Annotations;

namespace SportBuddy.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class GroupsController(IGroupRepository groupRepository, ICommandHandler<CreateGroupCommand> createGroupCommandHandler) : ControllerBase
{
    [HttpGet("{groupId:guid}")]
    [SwaggerOperation("Group with the given id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Group>> Get(Guid groupId)
    {
        var group = await groupRepository.GetAsync(groupId);
        return group is null ? NotFound() : Ok(group);
    }

    [HttpGet]
    [SwaggerOperation("List of all groups")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IEnumerable<Group>>> GetAll()
        => Ok(await groupRepository.GetAllAsync());

    [HttpPost]
    [SwaggerOperation("Create group")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize]
    public async Task<ActionResult> Post(CreateGroupCommand command)
    {
        var identityName = User.Identity?.Name;
        if (string.IsNullOrWhiteSpace(identityName))
        {
            return NotFound();
        }

        var userId = Guid.Parse(identityName);
        
        command = command with {GroupId = Guid.NewGuid(), AdminId = userId};
        await createGroupCommandHandler.HandleAsync(command);
        return CreatedAtAction(nameof(Get), new {command.GroupId}, null);
    }
    
    [HttpGet("{groupId:guid}/matches/archived")]
    [SwaggerOperation("List of archived matches in the group")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<Match>>> GetArchivedMatches(Guid groupId)
    {
        var group = await groupRepository.GetAsync(groupId);
        if (group is null)
        {
            return NotFound("Group not found");
        }

        throw new NotImplementedException();
    }

    [HttpGet("{groupId:guid}/matches/upcoming")]
    [SwaggerOperation("List of upcoming matches in the group")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<Match>>> GetUpcomingMatches(Guid groupId)
    {
        var group = await groupRepository.GetAsync(groupId);
        if (group is null)
        {
            return NotFound("Group not found");
        }
        
        throw new NotImplementedException();
    }

    [HttpPost("{groupId:guid}/leave")]
    [SwaggerOperation("Leave the group")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> LeaveGroup(Guid groupId)//, [FromBody] LeaveGroupRequest request)
    {
        var group = await groupRepository.GetAsync(groupId);
        if (group is null)
        {
            return NotFound("Group not found");
        }

        throw new NotImplementedException();
    }
}