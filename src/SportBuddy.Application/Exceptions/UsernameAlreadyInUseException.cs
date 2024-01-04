using SportBuddy.Core.Exceptions;

namespace SportBuddy.Application.Exceptions;

public sealed class UsernameAlreadyInUseException : CustomException
{
    public UsernameAlreadyInUseException(string username) : base($"Username: '{username}' is already in use.")
    {
    }
}