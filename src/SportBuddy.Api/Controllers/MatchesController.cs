﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportBuddy.Application.Commands.LeaveMatch;
using SportBuddy.Application.Commands.RegisterUserToMatch;
using SportBuddy.Application.DTO;
using SportBuddy.Application.Queries.GetMatch;
using SportBuddy.Application.Queries.GetMatchMembers;
using SportBuddy.Application.Queries.GetUserMatches;
using Swashbuckle.AspNetCore.Annotations;

namespace SportBuddy.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MatchesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("List of all authorized user matches by optional date from query or today")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<ActionResult<IEnumerable<MatchDto>>> GetMyMatches([FromQuery] DateOnly? date)
    {
        var identityName = User.Identity?.Name;
        if (string.IsNullOrWhiteSpace(identityName))
        {
            return NotFound();
        }

        return Ok(await mediator.Send(new GetUserMatchesQuery(Guid.Parse(identityName), date)));
    }
    
    [HttpGet("{matchId:guid}")]
    [SwaggerOperation("Match with the given id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MatchDto>> GetMatch(Guid matchId)
        => Ok(await mediator.Send(new GetMatchQuery(matchId)));
    
    [HttpPost("{matchId:guid}/register")]
    [SwaggerOperation("Register user to match")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize]
    public async Task<ActionResult> RegisterUserToMatch(Guid matchId)
    {
        var identityName = User.Identity?.Name;
        if (string.IsNullOrWhiteSpace(identityName))
        {
            return NotFound();
        }

        var command = new RegisterUserToMatchCommand(matchId, Guid.Parse(identityName));
        await mediator.Send(command);
        return NoContent();
    }
    
    [HttpDelete("{matchId:guid}/leave")]
    [SwaggerOperation("Leave match")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize]
    public async Task<ActionResult> LeaveMatch(Guid matchId)
    {
        var identityName = User.Identity?.Name;
        if (string.IsNullOrWhiteSpace(identityName))
        {
            return NotFound();
        }

        var command = new LeaveMatchCommand(matchId, Guid.Parse(identityName));
        await mediator.Send(command);
        return NoContent();
    }

    [HttpGet("{matchId:guid}/members")]
    [SwaggerOperation("List of match members")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetMatchMembers(Guid matchId)
    => Ok(await mediator.Send(new GetMatchMembersQuery(matchId)));
    
    [HttpPost("{matchId:guid}/guest/{id}")]
    [SwaggerOperation("Add a guest to the match")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult AddMatchGuest(Guid matchId, Guid id, [FromBody] string guestName)
    {
        throw new NotImplementedException();
    }
    
    [HttpDelete("{matchId:guid}/guest/{id}")]
    [SwaggerOperation("Remove a guest from the match")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult RemoveMatchGuest(Guid matchId, Guid id)
    {
        throw new NotImplementedException();
    }
}