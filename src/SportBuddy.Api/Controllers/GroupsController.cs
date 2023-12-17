using Microsoft.AspNetCore.Mvc;
using SportBuddy.Api.Entities;

namespace SportBuddy.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class GroupsController : ControllerBase
{
    private static readonly List<Group> _groups = new()
    {
        new Group("G1", "gruop 1", 11),
        new Group("G2", "gruop 2", 10),
        new Group("G3", "gruop 3 big", 15),
        new Group("G4", "gruop 4 small", 6),
    };

    [HttpGet("{groupId:guid}")]
    public ActionResult<Group> Get(Guid groupId)
    {
        var group = _groups.Find(x => x.Id == groupId);
        return group is null ? NotFound() : Ok(group);
    }

    [HttpGet]
    public ActionResult<IEnumerable<Group>> GetAll()
        => Ok(_groups);

    [HttpPost]
    public ActionResult Post(Group group)
    {
        _groups.Add(group);
        return NoContent();
    }
}