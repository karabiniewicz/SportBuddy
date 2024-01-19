using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportBuddy.Application.Abstractions;
using SportBuddy.Application.Commands.AddGroupMembers;
using SportBuddy.Application.Commands.CreateGroup;
using SportBuddy.Application.Commands.CreateMatch;
using SportBuddy.Application.DTO;
using SportBuddy.Application.Queries.GetGroupMembers;
using SportBuddy.Core.Entities;
using SportBuddy.Core.Repositories;
using SportBuddy.Core.ValueObjects;
using Swashbuckle.AspNetCore.Annotations;

namespace SportBuddy.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class GroupsController(
    IGroupRepository groupRepository,
    IUserRepository userRepository,
    ICommandHandler<CreateGroupCommand> createGroupCommandHandler,
    ICommandHandler<AddGroupMembersCommand> addGroupMembersCommandHandler,
    IQueryHandler<GetGroupMembersQuery, IEnumerable<UserDto>> getGroupMembersQueryHandler) : ControllerBase
{
    [HttpGet("{groupId:guid}")]
    [SwaggerOperation("Group with the given id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Group>> Get(Guid groupId)
    {
        var group = await groupRepository.GetAsync(groupId);
        return group is null ? NotFound() : Ok(group); 
        // TODO: return a GroupDto instead of Group
    }

    [HttpGet]
    [SwaggerOperation("List of all groups")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IEnumerable<Group>>> GetAll()
        => Ok(await groupRepository.GetAllAsync()); // TODO: return a GroupDto instead of Group

    [HttpPost]
    [SwaggerOperation("Create group")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize]
    public async Task<ActionResult> CreateGroup(CreateGroupCommand command)
    {
        var identityName = User.Identity?.Name;
        if (string.IsNullOrWhiteSpace(identityName))
        {
            return NotFound();
        }

        var userId = Guid.Parse(identityName);

        command = command with { Id = Guid.NewGuid(), AdminId = userId };
        await createGroupCommandHandler.HandleAsync(command);
        return CreatedAtAction(nameof(Get), new { GroupId = command.Id }, null);
    }

    [HttpPost("{groupId:guid}/users")]
    [SwaggerOperation("Add users to the group members")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize]
    public async Task<ActionResult> AddUsers(Guid groupId, AddGroupMembersCommand command)
    {
        command = command with { GroupId = groupId };
        await addGroupMembersCommandHandler.HandleAsync(command);
        return NoContent();
    }

    // TODO: consider what should happen if a group admin leaves the group
    // [HttpPost("{groupId:guid}/leave")]
    [HttpDelete("{groupId:guid}/leave")]
    [SwaggerOperation("Leave the group")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize]
    public async Task<ActionResult> LeaveGroup(Guid groupId)
    {
        var identityName = User.Identity?.Name;
        if (string.IsNullOrWhiteSpace(identityName))
        {
            return NotFound();
        }

        // TODO: move to leaveGroupCommandHandler
        var userId = new UserId(Guid.Parse(identityName));

        var group = await groupRepository.GetAsync(groupId);
        if (group is null)
        {
            return NotFound("Group not found");
        }

        if (group.Members.All(m => m.Id != userId))
        {
            return BadRequest("User is not a member of the group");
        }

        group.RemoveMember(userId);
        await groupRepository.UpdateAsync(group);

        return NoContent();
    }

    // TODO: only group admin should be able to invite users, and only users that are not already in the group
    [HttpGet("{groupId:guid}/usersToInvite")]
    [SwaggerOperation("List of all group members")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetGroupUsersToInvite(Guid groupId)
    {
        var identityName = User.Identity?.Name;
        if (string.IsNullOrWhiteSpace(identityName))
        {
            return NotFound();
        }

        // TODO: move to GetGroupUsersToInviteQueryHandler
        var userId = new UserId(Guid.Parse(identityName));

        var group = await groupRepository.GetAsync(groupId);
        if (group is null)
        {
            return NotFound("Group not found");
        }

        if (group.AdminId != userId)
        {
            return Forbid();
        }

        var groupMembers = group.Members.Select(m => m.Id);
        var users = await userRepository.GetUsersToInviteAsync(groupMembers);
        return Ok(users);
        // TODO: return a UserDto instead of User
    }

    [HttpGet("{groupId:guid}/users")]
    [SwaggerOperation("List of all group members")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetGroupMembers(Guid groupId)
        => Ok(await getGroupMembersQueryHandler.HandleAsync(new GetGroupMembersQuery(groupId)));
    
    [HttpPost("{groupId:guid}/matches")]
    [SwaggerOperation("Create new match in the group")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize]
    public async Task<ActionResult> CreateMatch(Guid groupId, CreateMatchCommand command)
    {
        var identityName = User.Identity?.Name;
        if (string.IsNullOrWhiteSpace(identityName))
        {
            return NotFound();
        }

        var userId = new UserId(Guid.Parse(identityName));

        var group = await groupRepository.GetAsync(groupId);
        if (group is null)
        {
            return NotFound("Group not found");
        }

        if (group.AdminId != userId)
        {
            return Forbid();
        }

        // TODO
        throw new NotImplementedException();
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
}