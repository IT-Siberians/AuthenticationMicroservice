namespace Common.Helpers.PasswordHelpers;

/// <summary>
/// Статический класс содержащий константы ошибок исключений в NewPassword в валидации Request
/// </summary>
public static class NewPasswordValidationMessages
{
    /// <summary>
    /// Текст ошибки при попытке передать пустую строку в NewPassword
    /// </summary>
    public const string NEW_PASSWORD_EMPTY_ERROR = "The new password cannot be empty.";

    /// <summary>
    /// Текст ошибки при попытке создать NewPassword короче установленного
    /// </summary>
    public const string NEW_PASSWORD_MIN_LENGTH_ERROR = "The password must contain at least 8 characters.";

    /// <summary>
    /// Текст ошибки при попытке создать NewPassword без заглавной буквы
    /// </summary>
    public const string NEW_PASSWORD_NOT_EXIST_CAPITAL_LETTER_ERROR = "The password must contain at least one capital letter.";

    /// <summary>
    /// Текст ошибки при попытке создать NewPassword без строчной буквы
    /// </summary>
    public const string NEW_PASSWORD_NOT_EXIST_LOWERCASE_LETTER_ERROR = "The password must contain at least one lowercase letter.";

    /// <summary>
    /// Текст ошибки при попытке создать NewPassword без цифры
    /// </summary>
    public const string NEW_PASSWORD_NOT_EXIST_DIGIT_ERROR = "The password must contain at least one digit.";

    /// <summary>
    /// Текст ошибки при попытке создать NewPassword без спецсимвола
    /// </summary>
    public const string NEW_PASSWORD_NOT_EXIST_SPECIAL_SYMBOL_ERROR = "The password must contain at least one special character.";
}