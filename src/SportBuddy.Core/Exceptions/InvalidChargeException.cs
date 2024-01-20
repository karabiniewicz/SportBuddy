namespace SportBuddy.Core.Exceptions;

public sealed class InvalidChargeException(decimal? charge) : CustomException($"Charge: '{charge}' is invalid.");