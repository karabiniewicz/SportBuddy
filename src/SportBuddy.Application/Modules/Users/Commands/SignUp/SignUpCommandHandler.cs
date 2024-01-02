using SportBuddy.Application.Abstractions;
using SportBuddy.Application.Security;
using SportBuddy.Core.Entities;

namespace SportBuddy.Application.Modules.Users.Commands.SignUp;

internal sealed class SignUpCommandHandler(IPasswordManager passwordManager) : ICommandHandler<SignUpCommand>
{
    public async Task HandleAsync(SignUpCommand command)
    {
        var (userId, email, username, password, fullName, role) = command;
        
        // TODO: valid username and password
        
        var securedPassword = passwordManager.Secure(password);
        var user = new User(userId, email, username, securedPassword, fullName, role, DateTime.Now);
        
        // TODO: add to db
    }
}