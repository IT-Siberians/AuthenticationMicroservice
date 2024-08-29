namespace Common.Helpers.PasswordHelpers;

/// <summary>
/// Статический класс содержащий константы ошибок исключений в Old Password в валидации Request
/// </summary>
public static class OldPasswordValidationMessages
{
    /// <summary>
    /// Текст ошибки при попытке передать пустую строку в OldPassword
    /// </summary>
    public const string OLD_PASSWORD_EMPTY_ERROR = "OldPassword should not be empty.";
}