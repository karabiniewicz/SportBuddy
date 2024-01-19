using SportBuddy.Application.Abstractions;
using SportBuddy.Core.ValueObjects;

namespace SportBuddy.Application.Commands.LeaveGroup;

public sealed record LeaveGroupCommand(GroupId GroupId, UserId UserId) : ICommand;