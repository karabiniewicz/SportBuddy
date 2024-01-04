using SportBuddy.Application.Abstractions;
using SportBuddy.Application.Exceptions;
using SportBuddy.Application.Security;
using SportBuddy.Core.Entities;
using SportBuddy.Core.Repositories;
using SportBuddy.Core.ValueObjects;

namespace SportBuddy.Application.Modules.Users.Commands.SignUp;

internal sealed class SignUpCommandHandler(IUserRepository userRepository, IPasswordManager passwordManager) : ICommandHandler<SignUpCommand>
{
    public async Task HandleAsync(SignUpCommand command)
    {
        var (userId, email, username, password, fullName, commandRole) = command;
        var role = string.IsNullOrWhiteSpace(commandRole) ? Role.User() : new Role(commandRole);

        if (await userRepository.GetByEmailAsync(email) is not null)
        {
            throw new EmailAlreadyInUseException(email);
        }

        if (await userRepository.GetByUsernameAsync(username) is not null)
        {
            throw new UsernameAlreadyInUseException(username);
        }

        var securedPassword = passwordManager.Secure(password);
        var user = new User(userId, email, username, securedPassword, fullName, role, DateTime.Now); // TODO: use TimeProvider 

        await userRepository.AddAsync(user);
    }
}