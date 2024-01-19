using SportBuddy.Core.Exceptions;

namespace SportBuddy.Application.Exceptions;

public sealed class UserIsNotMemberOfGroupException(Guid userId, Guid groupId)
    : CustomException($"User with id: '{userId}' is not a member of group with id: '{groupId}'.");