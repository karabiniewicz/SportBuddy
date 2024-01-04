namespace SportBuddy.Core.Exceptions;

public sealed class InvalidEmailException : CustomException
{
    public InvalidEmailException(string email) : base($"Email: '{email}' is invalid.")
    {
    }
}