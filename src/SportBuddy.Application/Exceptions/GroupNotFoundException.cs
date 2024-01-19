using SportBuddy.Core.Exceptions;

namespace SportBuddy.Application.Exceptions;

public sealed class GroupNotFoundException(Guid groupId) : CustomException($"Group with id: '{groupId}' was not found.");