using SportBuddy.Core.Exceptions;

namespace SportBuddy.Application.Exceptions;

public sealed class UserAlreadyMemberOfGroupException(Guid userId, Guid groupId)
    : CustomException($"User with id: '{userId}' is already a member of group with id: '{groupId}'.");