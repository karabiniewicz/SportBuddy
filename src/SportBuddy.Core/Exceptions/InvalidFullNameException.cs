namespace SportBuddy.Core.Exceptions;

public sealed class InvalidFullNameException : CustomException
{
    public InvalidFullNameException(string fullName) : base($"Full name: '{fullName}' is invalid.")
    {
    }
}