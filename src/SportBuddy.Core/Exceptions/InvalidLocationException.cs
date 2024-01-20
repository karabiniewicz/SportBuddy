namespace SportBuddy.Core.Exceptions;


public sealed class InvalidLocationException(string location) : CustomException($"Location: '{location}' is invalid.");