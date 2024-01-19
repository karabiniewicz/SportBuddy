using SportBuddy.Core.Exceptions;

namespace SportBuddy.Application.Exceptions;

public sealed class UserNotFoundException(Guid userId) : CustomException($"User with id: '{userId}' was not found.");