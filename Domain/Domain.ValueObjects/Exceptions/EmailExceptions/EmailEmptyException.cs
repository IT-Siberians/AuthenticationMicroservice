namespace Domain.ValueObjects.Exceptions.EmailExceptions;

/// <summary>
/// Исключительная ситуация создание пустого Email
/// </summary>
/// <param name="value">Создаваемый Email</param>
internal class EmailEmptyException(string value)
    : ArgumentNullException(paramName: nameof(value), message: ErrorDomainMessages.EMAIL_EMPTY_ERROR);