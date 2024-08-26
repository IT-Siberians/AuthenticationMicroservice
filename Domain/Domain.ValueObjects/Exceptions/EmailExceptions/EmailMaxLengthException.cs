namespace Domain.ValueObjects.Exceptions.EmailExceptions;

/// <summary>
/// Исключительная ситуация создание Email длиннее допустимого
/// </summary>
/// <param name="value">Длина создаваемого Email</param>
internal class EmailMaxLengthException(int value) : ArgumentOutOfRangeException(paramName: nameof(value), value,
    ErrorDomainMessages.EMAIL_LONGER_MAX_LENGTH_ERROR);
