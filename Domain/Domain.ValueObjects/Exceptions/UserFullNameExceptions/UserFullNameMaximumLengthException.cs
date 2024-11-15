using static Common.Helpers.Constants.UserFullnameConstants;
using static Domain.Helpers.UserFullNameHelpers.UserFullnameDomainMessages;

namespace Domain.ValueObjects.Exceptions.UserFullNameExceptions;

/// <summary>
/// Исключение, которое выбрасывается, если длина фамилии или имени пользователя превышает максимально допустимую.
/// </summary>
/// <param name="nameType">Тип имени (например, "FirstName" или "LastName").</param>
/// <param name="valueLength">Текущая длина значения имени или фамилии.</param>
/// <param name="paramName">Имя параметра, который имеет ошибку (например, "value").</param>
public class UserFullNameMaximumLengthException(
    string nameType,
    int valueLength,
    string paramName) 
    : ArgumentOutOfRangeException(
        paramName: paramName,
        string.Format(
            USER_FULLNAME_LONGER_MAX_LENGTH_ERROR,
            nameType,
            USER_FULLNAME_MAX_LENGTH,
            valueLength));