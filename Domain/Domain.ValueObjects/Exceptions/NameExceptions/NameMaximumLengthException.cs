using static Common.Helpers.Constants.NameConstants;
using static Domain.Helpers.NameHelpers.NameDomainMessages;

namespace Domain.ValueObjects.Exceptions.NameExceptions;

/// <summary>
/// Исключение, которое выбрасывается, если длина фамилии или имени пользователя превышает максимально допустимую.
/// </summary>
/// <param name="nameType">Тип имени (например, "FirstName" или "LastName").</param>
/// <param name="valueLength">Текущая длина значения имени или фамилии.</param>
/// <param name="paramName">Имя параметра, который имеет ошибку (например, "value").</param>
public class NameMaximumLengthException(
    string nameType,
    int valueLength,
    string paramName)
    : ArgumentOutOfRangeException(
        paramName: paramName,
        string.Format(
            NAME_LONGER_MAX_LENGTH_ERROR,
            nameType,
            NAME_MAX_LENGTH,
            valueLength));