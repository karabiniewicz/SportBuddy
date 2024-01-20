using SportBuddy.Application.Abstractions;
using SportBuddy.Core.ValueObjects;

namespace SportBuddy.Application.Commands.RegisterUserToMatch;

public record RegisterUserToMatchCommand(MatchId MatchId, UserId UserId) : ICommand;