using SportBuddy.Core.Exceptions;

namespace SportBuddy.Application.Exceptions;

public sealed class GroupNameAlreadyInUseException(string name) : CustomException($"Group name: '{name}' is already in use.");