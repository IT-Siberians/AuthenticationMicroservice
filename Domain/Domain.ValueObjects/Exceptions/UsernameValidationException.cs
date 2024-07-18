namespace Domain.ValueObjects.Exceptions;

public class UsernameValidationException(string message) : Exception(message);