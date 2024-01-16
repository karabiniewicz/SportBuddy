namespace SportBuddy.Core.Exceptions;

public sealed class InvalidRoleException(string role) : CustomException($"Role: '{role}' is invalid.");