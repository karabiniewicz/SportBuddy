using Microsoft.AspNetCore.Mvc;
using SportBuddy.Application.Commands;
using SportBuddy.Application.Commands.CreateMatch;
using SportBuddy.Application.DTO;
using SportBuddy.Core.Consts;
using SportBuddy.Core.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace SportBuddy.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MatchesController : ControllerBase
{
    private static readonly List<Match> Matches = new()
    {
        new Match("don balon pon", Discipline.Football, DateTimeOffset.Now),
        new Match("orlik hellera", Discipline.Football, DateTimeOffset.Now.AddDays(2)),
        new Match("orlik hellera", Discipline.Basketball, DateTimeOffset.Now.AddDays(5)),
    };

    [HttpGet("{matchId:guid}")]
    [SwaggerOperation("Match with the given id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<MatchDto> Get(Guid matchId)
    {
        var match = Matches.Find(x => x.Id == matchId);
        return match is null ? NotFound() : Ok(match.AsDto());
    }
    
    [HttpPost]
    [SwaggerOperation("Create match")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult Post(CreateMatchCommand command)
    {
        var (name, discipline, dateTimeOffset) = command;
        var match = new Match(name, discipline, dateTimeOffset);
        Matches.Add(match);
        return NoContent();
    }

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
    
    [HttpGet]
    [SwaggerOperation("List of matches")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public ActionResult<IEnumerable<Match>> GetMatches()//[FromQuery] GetMatchesQuery query)
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