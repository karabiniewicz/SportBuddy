using SportBuddy.Core.Exceptions;

namespace SportBuddy.Application.Exceptions;

public sealed class UsernameAlreadyInUseException(string username) : CustomException($"Username: '{username}' is already in use.");