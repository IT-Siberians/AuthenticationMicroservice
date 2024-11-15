namespace Domain.Helpers.UserFullNameHelpers;

/// <summary>
/// Класс для хранения сообщений, связанных с валидацией имени и фамилии пользователя.
/// </summary>
public class UserFullnameDomainMessages
{
    /// <summary>
    /// Сообщение об ошибке, когда имя или фамилия пустые, или равны null.
    /// </summary>
    public const string USER_FULLNAME_EMPTY_ERROR =
        "{0} cannot null or empty.";

    /// <summary>
    /// Сообщение об ошибке, когда длина имени или фамилии превышает максимальную допустимую.
    /// </summary>
    public const string USER_FULLNAME_LONGER_MAX_LENGTH_ERROR =
        "The {0} is longer than the allowed length of the user name. Max length - {1}, current length - {2}";

    /// <summary>
    /// Сообщение об ошибке, когда имя или фамилия не соответствуют формату.
    /// </summary>
    public const string USER_FULLNAME_FORMAT_ERROR =
        "The {0} value {1} does not match the format checked by the regular expression pattern {2}.";
}