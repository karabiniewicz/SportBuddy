using SportBuddy.Application.Abstractions;
using SportBuddy.Application.Exceptions;
using SportBuddy.Application.Security;
using SportBuddy.Core.Repositories;

namespace SportBuddy.Application.Commands.SIgnIn;

internal sealed class SignInCommandHandler(
    IUserRepository userRepository,
    IAuthenticator authenticator,
    IPasswordManager passwordManager,
    ITokenStorage tokenStorage) : ICommandHandler<SignInCommand>
{
    public async Task Handle(SignInCommand command, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetByEmailAsync(command.Email) ?? throw new InvalidCredentialsException();

        if (!passwordManager.Validate(command.Password, user.Password))
        {
            throw new InvalidCredentialsException();
        }

        var jwt = authenticator.CreateToken(user.Id, user.Role);
        tokenStorage.Set(jwt);
    }
}