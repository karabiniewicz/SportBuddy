using SportBuddy.Application.Abstractions;
using SportBuddy.Core.ValueObjects;

namespace SportBuddy.Application.Commands.LeaveMatch;

public sealed record LeaveMatchCommand(MatchId MatchId, UserId UserId) : ICommand;