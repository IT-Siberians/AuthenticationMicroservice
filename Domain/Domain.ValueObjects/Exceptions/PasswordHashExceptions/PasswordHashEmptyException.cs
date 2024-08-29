using static Common.Helpers.PasswordHashHelpers.PasswordHashDomainMessages;

namespace Domain.ValueObjects.Exceptions.PasswordHashExceptions;

/// <summary>
/// Исключительная ситуация создание пустого Хэшированного пароля
/// </summary>
/// <param name="paramName">Название параметра, в котором произошло исключение</param>
internal class PasswordHashEmptyException(string paramName)
    : ArgumentNullException(
        paramName: paramName,
        message: PASSWORDHASH_EMPTY_ERROR);