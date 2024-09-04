using static Domain.Helpers.UsernameHelpers.UsernameDomainMessages;

namespace Domain.ValueObjects.Exceptions.UsernameExceptions;

/// <summary>
/// Исключительная ситуация создание пустого Имени пользователя
/// </summary>
/// <param name="paramName">Название параметра, в котором произошло исключение</param>
internal class UsernameEmptyException(string paramName)
        : ArgumentNullException(
            paramName: paramName,
            message: USERNAME_EMPTY_STRING_ERROR);