namespace Domain.ValueObjects.Exceptions.UsernameExceptions;

/// <summary>
/// Исключительная ситуация создание Имени пользователя короче допустимого
/// </summary>
/// <param name="value">Длина создаваемого имени пользователя</param>
internal class UsernameMinLengthException(int value) : ArgumentOutOfRangeException(paramName: nameof(value), value,
    ErrorDomainMessages.USERNAME_SHORTER_MIN_LENGTH_ERROR);