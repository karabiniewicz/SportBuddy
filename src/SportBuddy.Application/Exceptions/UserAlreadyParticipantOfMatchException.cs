using SportBuddy.Core.Exceptions;

namespace SportBuddy.Application.Exceptions;

public sealed class UserAlreadyParticipantOfMatchException(Guid userId, Guid matchId)
    : CustomException($"User with id: '{userId}' is already a participant of match with id: '{matchId}'.");
