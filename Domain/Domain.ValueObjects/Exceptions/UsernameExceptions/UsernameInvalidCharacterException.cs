namespace Domain.ValueObjects.Exceptions.UsernameExceptions;

/// <summary>
/// Исключительная ситуация создание Имени пользователя с некорректными символами
/// </summary>
internal class UsernameInvalidCharacterException() : FormatException(ErrorDomainMessages.USERNAME_INVALID_CHARACTER_ERROR);
