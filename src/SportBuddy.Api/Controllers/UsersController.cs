using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportBuddy.Application.Abstractions;
using SportBuddy.Application.Commands.SIgnIn;
using SportBuddy.Application.Commands.SignUp;
using SportBuddy.Application.DTO;
using SportBuddy.Application.Queries.GetUser;
using SportBuddy.Application.Security;

namespace SportBuddy.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController(
    ICommandHandler<SignUpCommand> signUpCommandHandler,
    IQueryHandler<GetUserQuery, UserDto> getUserQueryHandler,
    ICommandHandler<SignInCommand> signInCommandHandler,
    ITokenStorage tokenStorage) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> SignUp(SignUpCommand command)
    {
        command = command with { UserId = Guid.NewGuid() };
        await signUpCommandHandler.HandleAsync(command);
        return NoContent();
    }

    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpGet("me")]
    public async Task<ActionResult<UserDto>> Get()
    {
        if (string.IsNullOrWhiteSpace(User.Identity?.Name))
        {
            return NotFound();
        }

        var userId = Guid.Parse(User.Identity?.Name);
        var user = await getUserQueryHandler.HandleAsync(new GetUserQuery(userId));

        return user;
    }

    [HttpPost("sign-in")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<JwtDto>> Post(SignInCommand command)
    {
        await signInCommandHandler.HandleAsync(command);
        var jwt = tokenStorage.Get();
        return jwt;
    }
}