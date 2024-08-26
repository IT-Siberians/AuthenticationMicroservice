namespace Domain.ValueObjects.Exceptions.UsernameExceptions;

/// <summary>
/// Исключительная ситуация создание Имени пользователя длиннее допустимого
/// </summary>
/// <param name="value">Длина создаваемого имени пользователя</param>
internal class UsernameMaxLengthException(int value) : ArgumentOutOfRangeException(paramName: nameof(value), value,
    ErrorDomainMessages.USERNAME_LONGER_MAX_LENGTH_ERROR);