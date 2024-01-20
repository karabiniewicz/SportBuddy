using SportBuddy.Core.Exceptions;

namespace SportBuddy.Application.Exceptions;

public sealed class UserIsNotAdminException(Guid userId, Guid groupId)
    : CustomException($"User with id: '{userId}' is not an admin of group with id: '{groupId}'.");
