namespace Domain.ValueObjects.Exceptions.PasswordHashExceptions;

/// <summary>
/// Исключительная ситуация создание пустого Хэшированного пароля
/// </summary>
/// <param name="value">Создаваемый Хэшированный пароль</param>
internal class PasswordHashEmptyException(string value)
    : ArgumentNullException(paramName: nameof(value), message: ErrorDomainMessages.PASSWORDHASH_EMPTY_ERROR);

