using SportBuddy.Core.Exceptions;

namespace SportBuddy.Application.Exceptions;

public sealed class EmailAlreadyInUseException(string email) : CustomException($"Email: '{email}' is already in use.");