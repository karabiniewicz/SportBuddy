using Microsoft.AspNetCore.Mvc;
using SportBuddy.Application.Abstractions;
using SportBuddy.Application.Commands;
using SportBuddy.Application.DTO;
using SportBuddy.Application.Queries.GetMatch;
using SportBuddy.Core.Entities;
using SportBuddy.Core.Repositories;
using Swashbuckle.AspNetCore.Annotations;

namespace SportBuddy.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MatchesController(IMatchRepository matchRepository, IQueryHandler<GetMatchQuery, MatchDto> getMatchQueryHandler) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("List of matches")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IEnumerable<Match>>> GetMatches()
        => Ok((await matchRepository.GetAllAsync()).Select(x => x.AsDto()));

    [HttpGet("{matchId:guid}")]
    [SwaggerOperation("Match with the given id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MatchDto>> GetMatch(Guid matchId)
        => Ok(await getMatchQueryHandler.HandleAsync(new GetMatchQuery(matchId)));
    
    
    [HttpGet("{matchId:guid}/members")]
    [SwaggerOperation("List of match members")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<UserDto>> GetMatchMembers(Guid matchId)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost("{matchId:guid}/members/{id:guid}")]
    [SwaggerOperation("Add a member to the match")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult AddMatchMember(Guid matchId, Guid id)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost("{matchId:guid}/guest/{id}")]
    [SwaggerOperation("Add a guest to the match")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult AddMatchGuest(Guid matchId, Guid id, [FromBody] string guestName)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{matchId:guid}/members/{id}")]
    [SwaggerOperation("Remove a member from the match")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult RemoveMatchMember(Guid matchId, Guid id)
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