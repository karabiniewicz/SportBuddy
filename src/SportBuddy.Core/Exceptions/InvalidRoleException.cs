namespace SportBuddy.Core.Exceptions;

public sealed class InvalidRoleException : CustomException
{
    public InvalidRoleException(string role) : base($"Role: '{role}' is invalid.")
    {
    }
}