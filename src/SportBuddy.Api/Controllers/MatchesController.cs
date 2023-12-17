using Microsoft.AspNetCore.Mvc;
using SportBuddy.Api.Consts;
using SportBuddy.Api.Entities;

namespace SportBuddy.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MatchesController : ControllerBase
{
    private static readonly List<Match> _matches = new()
    {
        new Match("don balon pon", DateTimeOffset.Now, Discipline.Football),
        new Match("orlik hellera", DateTimeOffset.Now.AddDays(2), Discipline.Football),
        new Match("orlik hellera", DateTimeOffset.Now.AddDays(5), Discipline.Basketball)
    };

    [HttpGet("{matchId:guid}")]
    public ActionResult<Match> Get(Guid matchId)
    {
        var match = _matches.Find(x => x.Id == matchId);
        return match is null ? NotFound() : Ok(match);
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<Match>> GetAll()
        => Ok(_matches);
    
    [HttpPost]
    public ActionResult Post(Match match)
    {
        _matches.Add(match);
        return NoContent();
    }
}