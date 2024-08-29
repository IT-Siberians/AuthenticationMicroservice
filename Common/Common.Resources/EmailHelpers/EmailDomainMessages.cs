namespace Common.Helpers.EmailHelpers;

/// <summary>
/// Статический класс содержащий константы ошибок исключений в Email в доменном слое
/// </summary>
public static class EmailDomainMessages
{
    /// <summary>
    /// Текст ошибки при попытке создать пустом Email
    /// </summary>
    public const string EMAIL_EMPTY_ERROR = "Email cannot null or empty.";

    /// <summary>
    /// Текст ошибки при попытке создать Email длиннее установленного
    /// </summary>
    public const string EMAIL_LONGER_MAX_LENGTH_ERROR = "The Email is longer than the allowed length of the user name. Max length - {0}, current length - {1}";

    /// <summary>
    /// Текст ошибки при попытке создать Email в недопустимом формате
    /// </summary>
    public const string EMAIL_FORMAT_ERROR = "Invalid Email address format. {0} not correct"; }