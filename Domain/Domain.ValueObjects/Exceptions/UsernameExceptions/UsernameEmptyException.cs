namespace Domain.ValueObjects.Exceptions.UsernameExceptions;

/// <summary>
/// Исключительная ситуация создание пустого Имени пользователя
/// </summary>
/// <param name="value">Создаваемое имя пользователя/param>
internal class UsernameEmptyException(string value)
        : ArgumentNullException(paramName: nameof(value), message: ErrorDomainMessages.USERNAME_EMPTY_STRING_ERROR);
