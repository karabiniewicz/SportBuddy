using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportBuddy.Application.Abstractions;
using SportBuddy.Application.Commands.SIgnIn;
using SportBuddy.Application.Commands.SignUp;
using SportBuddy.Application.DTO;
using SportBuddy.Application.Queries.GetUser;
using SportBuddy.Application.Queries.GetUsers;
using SportBuddy.Application.Security;
using Swashbuckle.AspNetCore.Annotations;

namespace SportBuddy.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController(
    ICommandHandler<SignUpCommand> signUpCommandHandler,
    IQueryHandler<GetUserQuery, UserDto> getUserQueryHandler,
    IQueryHandler<GetUsersQuery, IEnumerable<UserDto>> getUsersQueryHandler,
    ICommandHandler<SignInCommand> signInCommandHandler,
    ITokenStorage tokenStorage) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation("Create user account")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> SignUp(SignUpCommand command)
    {
        command = command with { UserId = Guid.NewGuid() };
        await signUpCommandHandler.HandleAsync(command);
        return CreatedAtAction(nameof(Get), new {userId = command.UserId}, null);
    }
    
    [HttpPost("sign-in")]
    [SwaggerOperation("Sign in user and return the JSON Web Token")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<JwtDto>> SignIn(SignInCommand command)
    {
        await signInCommandHandler.HandleAsync(command);
        var jwt = tokenStorage.Get();
        return jwt;
    }

    [HttpGet("me")]
    [SwaggerOperation("Currently logged in user")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    public async Task<ActionResult<UserDto>> GetMe()
    {
        var identityName = User.Identity?.Name;
        if (string.IsNullOrWhiteSpace(identityName))
        {
            return NotFound();
        }

        var userId = Guid.Parse(identityName);
        var user = await getUserQueryHandler.HandleAsync(new GetUserQuery(userId));

        return user;
    }
    
    [HttpGet]
    [SwaggerOperation("List of all users")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAll([FromQuery] GetUsersQuery query)
        => Ok(await getUsersQueryHandler.HandleAsync(query));
    
    [HttpGet("{userId:guid}")]
    [SwaggerOperation("User with the given id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    // TODO: in future only admins [Authorize(Policy = "is-admin")]
    public async Task<ActionResult<UserDto>> Get(Guid userId)
    {
        var user = await getUserQueryHandler.HandleAsync(new GetUserQuery(userId));
        if (user is null)
        {
            return NotFound();
        }

        return user;
    }

    [HttpPost("refresh")]
    [SwaggerOperation("Refresh access token")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize]
    public ActionResult<JwtDto> RefreshToken()
    {
        throw new NotImplementedException();
    }

}