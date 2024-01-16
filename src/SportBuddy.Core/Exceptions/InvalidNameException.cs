namespace SportBuddy.Core.Exceptions;

public sealed class InvalidNameException(string name) : CustomException($"Name: '{name}' is invalid.");