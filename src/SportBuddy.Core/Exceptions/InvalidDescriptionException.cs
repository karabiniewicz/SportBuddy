namespace SportBuddy.Core.Exceptions;

public sealed class InvalidDescriptionException(string name) : CustomException($"Description: '{name}' is invalid.");