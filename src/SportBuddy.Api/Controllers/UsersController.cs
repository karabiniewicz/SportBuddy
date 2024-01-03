using Microsoft.AspNetCore.Mvc;
using SportBuddy.Application.Abstractions;
using SportBuddy.Application.Modules.Users.Commands.SignUp;

namespace SportBuddy.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController(ICommandHandler<SignUpCommand> signUpCommandHandler) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> Post(SignUpCommand command)
    {
        command = command with {UserId = Guid.NewGuid()};
        await signUpCommandHandler.HandleAsync(command);
        return NoContent();
    }
}