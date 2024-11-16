using static Domain.Helpers.UserFullNameHelpers.UserFullnameDomainMessages;

namespace Domain.ValueObjects.Exceptions.UserFullNameExceptions;

/// <summary>
/// Исключение, которое выбрасывается, если фамилия или имя пользователя не соответствует ожидаемому формату.
/// </summary>
/// <param name="nameType">Тип имени (например, "FirstName" или "LastName").</param>
/// <param name="firstNameValue">Значение имени или фамилии, которое не прошло валидацию.</param>
/// <param name="formatPattern">Регулярное выражение, которое описывает требуемый формат.</param>
public class UserFullNameFormatException(
    string nameType,
    string firstNameValue,
    string formatPattern)
        : FormatException(
            string.Format(
                USER_FULLNAME_FORMAT_ERROR,
                nameType,
                firstNameValue,
                formatPattern));