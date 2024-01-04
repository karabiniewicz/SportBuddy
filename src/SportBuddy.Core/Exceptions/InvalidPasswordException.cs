namespace SportBuddy.Core.Exceptions;

public sealed class InvalidPasswordException : CustomException
{
    public InvalidPasswordException() : base("Invalid password. Minimum 6 characters.")
    {
    }
}