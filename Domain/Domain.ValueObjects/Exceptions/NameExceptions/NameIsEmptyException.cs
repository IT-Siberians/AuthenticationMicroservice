using static Domain.Helpers.NameHelpers.NameDomainMessages;

namespace Domain.ValueObjects.Exceptions.NameExceptions;

/// <summary>
/// Исключение, которое выбрасывается, если фамилия или имя пользователя пустое или равно null.
/// </summary>
/// <param name="nameType">Тип имени (например, "FirstName" или "LastName").</param>
/// <param name="paramName">Имя параметра, который имеет ошибку (например, "value").</param>
internal class NameIsEmptyException(
    string nameType,
    string paramName)
    : ArgumentNullException(
        paramName: paramName,
        string.Format(
            NAME_EMPTY_ERROR,
            nameType));