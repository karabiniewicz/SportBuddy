using Microsoft.AspNetCore.Mvc;
using SportBuddy.Api.Commands;
using SportBuddy.Api.Consts;
using SportBuddy.Api.DTO;
using SportBuddy.Api.Entities;

namespace SportBuddy.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MatchesController : ControllerBase
{
    private static readonly List<Match> _matches = new()
    {
        new Match("don balon pon", Discipline.Football, DateTimeOffset.Now, "g1"),
        new Match("orlik hellera", Discipline.Football, DateTimeOffset.Now.AddDays(2), "g1"),
        new Match("orlik hellera", Discipline.Basketball, DateTimeOffset.Now.AddDays(5), "g1"),
    };

    [HttpGet("{matchId:guid}")]
    public ActionResult<MatchDto> Get(Guid matchId)
    {
        var match = _matches.Find(x => x.Id == matchId);
        return match is null ? NotFound() : Ok(match.AsDto());
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<Match>> GetAll()
        => Ok(_matches);
    
    [HttpPost]
    public ActionResult Post(CreateMatchCommand command)
    {
        var (name, discipline, dateTimeOffset, groupName) = command;
        var match = new Match(name, discipline, dateTimeOffset, groupName);
        _matches.Add(match);
        return NoContent();
    }
}