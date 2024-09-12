namespace WebApiAuthenticate.RequestsValidators.Helpers.EmailHelpers;

/// <summary>
/// Статический класс содержащий константы ошибок исключений в Email в валидации Request
/// </summary>
public static class EmailValidationMessages
{
    /// <summary>
    /// Текст ошибки при попытке передать пустую строку в Email
    /// </summary>
    public const string EMAIL_EMPTY_ERROR =
        "The email cannot be empty.";

    /// <summary>
    /// Текст ошибки при попытке передать строку в Email длиннее установленного
    /// </summary>
    public const string EMAIL_LONGER_MAX_LENGTH_ERROR =
        "The email should be no more than {0}.";

    /// <summary>
    /// Текст ошибки при попытке передать строку в Email в недопустимом формате
    /// </summary>
    public const string EMAIL_FORMAT_ERROR =
        "Invalid email address format.";
}