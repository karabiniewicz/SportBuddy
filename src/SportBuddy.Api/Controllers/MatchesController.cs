using Microsoft.AspNetCore.Mvc;
using SportBuddy.Application.DTO;
using SportBuddy.Application.Modules.Matches.Commands;
using SportBuddy.Application.Modules.Matches.Commands.CreateMatch;
using SportBuddy.Core.Consts;
using SportBuddy.Core.Entities;

namespace SportBuddy.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MatchesController : ControllerBase
{
    private static readonly List<Match> Matches = new()
    {
        new Match("don balon pon", Discipline.Football, DateTimeOffset.Now, "g1"),
        new Match("orlik hellera", Discipline.Football, DateTimeOffset.Now.AddDays(2), "g1"),
        new Match("orlik hellera", Discipline.Basketball, DateTimeOffset.Now.AddDays(5), "g1"),
    };

    [HttpGet("{matchId:guid}")]
    public ActionResult<MatchDto> Get(Guid matchId)
    {
        var match = Matches.Find(x => x.Id == matchId);
        return match is null ? NotFound() : Ok(match.AsDto());
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<Match>> GetAll()
        => Ok(Matches);
    
    [HttpPost]
    public ActionResult Post(CreateMatchCommand command)
    {
        var (name, discipline, dateTimeOffset, groupName) = command;
        var match = new Match(name, discipline, dateTimeOffset, groupName);
        Matches.Add(match);
        return NoContent();
    }
}