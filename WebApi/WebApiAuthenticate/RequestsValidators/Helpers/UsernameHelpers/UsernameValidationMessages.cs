namespace WebApiAuthenticate.RequestsValidators.Helpers.UsernameHelpers;

/// <summary>
/// Статический класс содержащий константы ошибок исключений в Username в валидации Request
/// </summary>
public static class UsernameValidationMessages
{
    /// <summary>
    /// Текст ошибки при попытке передать пустую строку в Username
    /// </summary>
    public const string USERNAME_EMPTY_ERROR = 
        "The email cannot be empty.";

    /// <summary>
    /// Текст ошибки при попытке передать строку в Username с некорректной длиной
    /// </summary>
    public const string USERNAME_LENGTH_ERROR =
        "The username must be no more than {0} characters and no less than {1}.";

    /// <summary>
    /// Текст ошибки при попытке передать строку в Username в недопустимом формате
    /// </summary>
    public const string USERNAME_FORMAT_ERROR =
        "Invalid username format.";
}