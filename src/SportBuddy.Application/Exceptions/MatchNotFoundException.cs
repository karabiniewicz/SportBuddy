using SportBuddy.Core.Exceptions;

namespace SportBuddy.Application.Exceptions;

public sealed class MatchNotFoundException(Guid matchId) : CustomException($"Match with id: '{matchId}' was not found.");