namespace SportBuddy.Core.Exceptions;

public sealed class InvalidEntityIdException(object id) : CustomException($"Cannot set: {id}  as entity identifier.");