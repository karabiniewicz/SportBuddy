using Microsoft.AspNetCore.Mvc;
using SportBuddy.Core.Entities;
using SportBuddy.Core.Repositories;

namespace SportBuddy.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class GroupsController(IGroupRepository groupRepository) : ControllerBase
{
    [HttpGet("{groupId:guid}")]
    public async Task<ActionResult<Group>> Get(Guid groupId)
    {
        var group = await groupRepository.GetAsync(groupId);
        return group is null ? NotFound() : Ok(group);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Group>>> GetAll()
        => Ok(await groupRepository.GetAllAsync());

    [HttpPost]
    public async Task<ActionResult> Post(Group group)
    {
        await groupRepository.AddAsync(group);
        return NoContent();
    }
}