using static Common.Helpers.Constants.UsernameConstants;
using static Domain.Helpers.UsernameHelpers.UsernameDomainMessages;

namespace Domain.ValueObjects.Exceptions.UsernameExceptions;

/// <summary>
/// Исключительная ситуация создание Имени пользователя длиннее допустимого
/// </summary>
/// <param name="valueLength">Длина создаваемого Имени пользователя</param>
/// <param name="paramName">Название параметра, в котором произошло исключение</param>
internal class UsernameMaxLengthException(int valueLength, string paramName)
    : ArgumentOutOfRangeException(
        paramName: paramName,
        string.Format(USERNAME_LONGER_MAX_LENGTH_ERROR, USERNAME_MAX_LENGTH, valueLength));