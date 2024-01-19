using SportBuddy.Application.Abstractions;

namespace SportBuddy.Application.Commands.AddGroupMembers;

public sealed record AddGroupMembersCommand(Guid GroupId, IEnumerable<Guid> UserIds) : ICommand;