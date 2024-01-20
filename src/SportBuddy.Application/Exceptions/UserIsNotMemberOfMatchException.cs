using SportBuddy.Core.Exceptions;

namespace SportBuddy.Application.Exceptions;

public sealed class UserIsNotMemberOfMatchException(Guid matchId, Guid userId)
    : CustomException($"User with id: '{userId}' is not a member of match with id: '{matchId}'.");