using static Common.Helpers.Constants.PasswordHashConstants;
using static Domain.Helpers.PasswordHashHelpers.PasswordHashDomainMessages;

namespace Domain.ValueObjects.Exceptions.PasswordHashExceptions;

/// <summary>
/// Исключительная ситуация создание Email длиннее допустимого
/// </summary>
/// <param name="valueLength">Длина создаваемого Email</param>
/// <param name="paramName">Название параметра, в котором произошло исключение</param>
internal class PasswordMaxLengthException(int valueLength, string paramName)
    : ArgumentOutOfRangeException(
        paramName: paramName,
        string.Format(PASSWORDHASH_MAX_LENGTH_ERROR, PASSWORDHASH_MAX_LENGTH, valueLength));