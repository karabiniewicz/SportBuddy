namespace SportBuddy.Core.Exceptions;

public sealed class InvalidPasswordException() : CustomException("Invalid password. Minimum 6 characters.");