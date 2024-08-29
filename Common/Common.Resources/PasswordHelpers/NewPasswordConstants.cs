namespace Common.Helpers.PasswordHelpers;

/// <summary>
/// Статический класс содержащий константы параметров исключений в Email
/// </summary>
public static class NewPasswordConstants
{
    /// <summary>
    /// Минимальная длина NewPassword
    /// </summary>
    public const int NEW_PASSWORD_MIN_LENGTH = 8;

    /// <summary>
    /// Паттерн содержания заглавной буквы в NewPassword
    /// </summary>
    public const string NEW_PASSWORD_EXIST_CAPITAL_LETTER_PATTERN = "[A-Z]";

    /// <summary>
    /// Паттерн содержания строчной буквы в NewPassword
    /// </summary>
    public const string NEW_PASSWORD_EXIST_LOWERCASE_LETTER_PATTERN = "[a-z]";

    /// <summary>
    /// Паттерн содержания цифры в NewPassword
    /// </summary>
    public const string NEW_PASSWORD_EXIST_DIGIT_PATTERN = "[0-9]";

    /// <summary>
    /// Паттерн содержания спецсимвола в NewPassword
    /// </summary>
    public const string NEW_PASSWORD_EXIST_SPECIAL_SYMBOL_PATTERN = @"[!@#$%^&*(),.?""{}|<>]";
}
