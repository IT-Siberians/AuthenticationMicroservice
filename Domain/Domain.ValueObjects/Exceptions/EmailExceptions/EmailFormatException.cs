namespace Domain.ValueObjects.Exceptions.EmailExceptions;

/// <summary>
/// Исключительная ситуация создание Email с некорректным форматом
/// </summary>
internal class EmailFormatException() : FormatException(ErrorDomainMessages.EMAIL_FORMAT_ERROR);