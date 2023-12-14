using Microsoft.AspNetCore.Mvc;
using SportBuddy.Api.Entities;

namespace SportBuddy.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MatchesController : ControllerBase
{
    [HttpGet]
    public ActionResult<Match> Get()
        => Ok(new Match());

    [HttpPost]
    public void Post(Match match)
    {
        
    }
}