using Microsoft.AspNetCore.Mvc;
using SportBuddy.Core.Entities;
using SportBuddy.Core.Repositories;
using Swashbuckle.AspNetCore.Annotations;

namespace SportBuddy.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class GroupsController(IGroupRepository groupRepository) : ControllerBase
{
    [HttpGet("{groupId:guid}")]
    [SwaggerOperation("Group with the given id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Group>> Get(Guid groupId)
    {
        var group = await groupRepository.GetAsync(groupId);
        return group is null ? NotFound() : Ok(group);
    }

    [HttpGet]
    [SwaggerOperation("List of all groups")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IEnumerable<Group>>> GetAll()
        => Ok(await groupRepository.GetAllAsync());

    [HttpPost]
    [SwaggerOperation("Create group")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post(Group group)
    {
        await groupRepository.AddAsync(group);
        return NoContent();
    }
}