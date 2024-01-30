using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportBuddy.Application.Commands.AddGroupMembers;
using SportBuddy.Application.Commands.CreateGroup;
using SportBuddy.Application.Commands.CreateMatch;
using SportBuddy.Application.Commands.LeaveGroup;
using SportBuddy.Application.DTO;
using SportBuddy.Application.Queries.GetArchivedMatches;
using SportBuddy.Application.Queries.GetGroup;
using SportBuddy.Application.Queries.GetGroupMembers;
using SportBuddy.Application.Queries.GetGroupUsersToInvite;
using SportBuddy.Application.Queries.GetUpcomingMatches;
using SportBuddy.Application.Queries.GetUserGroups;
using SportBuddy.Core.Entities;
using SportBuddy.Core.ValueObjects;
using Swashbuckle.AspNetCore.Annotations;

namespace SportBuddy.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class GroupsController(IMediator mediator) : ControllerBase
{
    [HttpGet("{groupId:guid}")]
    [SwaggerOperation("Group with the given id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GroupDto>> Get(Guid groupId)
        => Ok(await mediator.Send(new GetGroupQuery(groupId)));
    
    [HttpGet]
    [SwaggerOperation("List of all user groups")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<ActionResult<IEnumerable<GroupDto>>> GetAll()
    {
        var identityName = User.Identity?.Name;
        if (string.IsNullOrWhiteSpace(identityName))
        {
            return NotFound();
        }

        var userId = Guid.Parse(identityName);
        var groups = await mediator.Send(new GetUserGroupsQuery(userId));
        return Ok(groups);
    }
    
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
        await mediator.Send(command);
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
        await mediator.Send(command);
        return NoContent();
    }

    // TODO: consider what should happen if a group admin leaves the group
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

        await mediator.Send(new LeaveGroupCommand(groupId, Guid.Parse(identityName)));

        return NoContent();
    }

    [HttpGet("{groupId:guid}/usersToInvite")]
    [SwaggerOperation("List of all users that can be invited to the group")]
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
        
        var userId = new UserId(Guid.Parse(identityName));
        var users = await mediator.Send(new GetGroupUsersToInviteQuery(groupId, userId));
        
        return Ok(users);
    }

    [HttpGet("{groupId:guid}/users")]
    [SwaggerOperation("List of all group members")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetGroupMembers(Guid groupId)
        => Ok(await mediator.Send(new GetGroupMembersQuery(groupId)));
    
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

        command = command with { Id = Guid.NewGuid(), GroupId = groupId, UserId = Guid.Parse(identityName) };
        await mediator.Send(command);
        
        // var actionName = nameof(MatchesController.GetMatch);
        // return CreatedAtAction(actionName, "Matches", new { matchId = command.Id }, null);
        return NoContent();
    }

    [HttpGet("{groupId:guid}/matches/archived")]
    [SwaggerOperation("List of archived matches in the group")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize]
    public async Task<ActionResult<IEnumerable<MatchDto>>> GetArchivedMatches(Guid groupId)
        => Ok(await mediator.Send(new GetArchivedMatchesQuery(groupId)));

    [HttpGet("{groupId:guid}/matches/upcoming")]
    [SwaggerOperation("List of upcoming matches in the group")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Match>>> GetUpcomingMatches(Guid groupId)
        => Ok(await mediator.Send(new GetUpcomingMatchesQuery(groupId)));
}