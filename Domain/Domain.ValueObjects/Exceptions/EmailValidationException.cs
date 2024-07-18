namespace Domain.ValueObjects.Exceptions;

public class EmailValidationException(string message) : Exception(message);
