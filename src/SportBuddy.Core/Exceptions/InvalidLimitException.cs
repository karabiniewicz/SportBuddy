namespace SportBuddy.Core.Exceptions;

public sealed class InvalidLimitException(int? limit) : CustomException($"Limit: '{limit}' is invalid.");