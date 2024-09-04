using static Common.Helpers.Constants.UsernameConstants;
using static Domain.Helpers.UsernameHelpers.UsernameDomainMessages;

namespace Domain.ValueObjects.Exceptions.UsernameExceptions;

/// <summary>
/// Исключительная ситуация создание Имени пользователя короче допустимого
/// </summary>
/// <param name="valueLength">Длина создаваемого Имени пользователя</param>
/// <param name="paramName">Название параметра, в котором произошло исключение</param>
internal class UsernameMinLengthException(int valueLength, string paramName) 
    : ArgumentOutOfRangeException(
        paramName: paramName,
        string.Format(USERNAME_SHORTER_MIN_LENGTH_ERROR, USERNAME_MAX_LENGTH, valueLength));