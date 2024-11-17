namespace Common.Helpers.Constants;

/// <summary>
/// Константы для проверки имени и фамилии пользователя.
/// </summary>
public static class NameConstants
{
    /// <summary>
    /// Максимальная длина для имени и фамилии.
    /// </summary>
    public const int NAME_MAX_LENGTH = 30;

    /// <summary>
    /// Регулярное выражение для проверки допустимых символов в имени или фамилии.
    /// </summary>
    public const string NAME_CHARACHTER_SET_PATTERN = "(^[a-zA-Z]+$)|(^[а-яА-Я]+$)";

    /// <summary>
    /// Регулярное выражение для проверки капитализации первой буквы в имени или фамилии.
    /// </summary>
    public const string NAME_CAPITALIZATION_PATTERN = "(^[A-Z].[a-z]*$)|(^[А-Я].[а-я]*$)";
}