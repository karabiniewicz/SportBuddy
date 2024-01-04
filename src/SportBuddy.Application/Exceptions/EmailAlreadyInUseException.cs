using SportBuddy.Core.Exceptions;

namespace SportBuddy.Application.Exceptions;

public sealed class EmailAlreadyInUseException : CustomException
{
    public EmailAlreadyInUseException(string email) : base($"Email: '{email}' is already in use.")
    {
    }
}