using SportBuddy.Application.Abstractions;
using SportBuddy.Application.Exceptions;
using SportBuddy.Application.Security;
using SportBuddy.Core.Entities;
using SportBuddy.Core.Repositories;
using SportBuddy.Core.ValueObjects;

namespace SportBuddy.Application.Commands.SignUp;

internal sealed class SignUpCommandHandler(IUserRepository userRepository, IPasswordManager passwordManager, TimeProvider timeProvider)
    : ICommandHandler<SignUpCommand>
{
    public async Task Handle(SignUpCommand command, CancellationToken cancellationToken = default)
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
        var user = new User(userId, email, username, securedPassword, fullName, role, timeProvider.GetLocalNow()); 

        await userRepository.AddAsync(user);
    }
}