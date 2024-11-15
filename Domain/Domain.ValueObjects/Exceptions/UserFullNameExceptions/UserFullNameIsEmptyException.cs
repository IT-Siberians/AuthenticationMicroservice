using static Domain.Helpers.UserFullNameHelpers.UserFullnameDomainMessages;

namespace Domain.ValueObjects.Exceptions.UserFullNameExceptions;

/// <summary>
/// Исключение, которое выбрасывается, если фамилия или имя пользователя пустое или равно null.
/// </summary>
/// <param name="nameType">Тип имени (например, "FirstName" или "LastName").</param>
/// <param name="paramName">Имя параметра, который имеет ошибку (например, "value").</param>
internal class UserFullNameIsEmptyException(
    string nameType,
    string paramName)
    : ArgumentNullException(
        paramName: paramName,
        string.Format(
            USER_FULLNAME_EMPTY_ERROR,
            nameType));