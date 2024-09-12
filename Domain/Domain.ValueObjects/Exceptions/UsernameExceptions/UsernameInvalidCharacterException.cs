using static Domain.Helpers.UsernameHelpers.UsernameDomainMessages;

namespace Domain.ValueObjects.Exceptions.UsernameExceptions;

/// <summary>
/// Исключительная ситуация создание Имени пользователя с некорректными символами
/// </summary>
/// <param name="usernameValue">Значение создаваемого Имени пользователя</param>
internal class UsernameInvalidCharacterException(string usernameValue)
    : FormatException(
        string.Format(USERNAME_INVALID_CHARACTER_ERROR, usernameValue));