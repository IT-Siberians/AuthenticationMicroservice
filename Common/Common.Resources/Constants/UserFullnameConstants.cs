namespace Common.Helpers.Constants;

/// <summary>
/// Константы для проверки имени и фамилии пользователя.
/// </summary>
public static class UserFullnameConstants
{
    /// <summary>
    /// Максимальная длина для имени и фамилии.
    /// </summary>
    public const int USER_FULLNAME_MAX_LENGTH = 30;

    /// <summary>
    /// Регулярное выражение для проверки допустимых символов в имени или фамилии.
    /// </summary>
    public const string USER_FULLNAME_CHARACHTER_SET_PATTERN = "(^[a-zA-Z]+$)|(^[а-яА-Я]+$)";

    /// <summary>
    /// Регулярное выражение для проверки капитализации первой буквы в имени или фамилии.
    /// </summary>
    public const string USER_FULLNAME_CAPITALIZATION_PATTERN = "(^[A-Z].[a-z]*$)|(^[А-Я].[а-я]*$)";
}