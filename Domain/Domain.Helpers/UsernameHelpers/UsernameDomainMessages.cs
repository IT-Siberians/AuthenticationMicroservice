namespace Domain.Helpers.UsernameHelpers;

/// <summary>
/// Статический класс содержащий константы ошибок исключений в Username в доменном слое
/// </summary>
public static class UsernameDomainMessages
{
    /// <summary>
    /// Текст ошибки при попытке создать при пустом Username
    /// </summary>
    public const string USERNAME_EMPTY_STRING_ERROR =
        "Username cannot null or empty.";

    /// <summary>
    /// Текст ошибки при попытке создать Username длиннее установленного
    /// </summary>
    public const string USERNAME_LONGER_MAX_LENGTH_ERROR =
        "The Username is longer than the allowed length of the user name. Max length - {0}, current length - {1}";

    /// <summary>
    /// Текст ошибки при попытке создать Username короче установленного
    /// </summary>
    public const string USERNAME_SHORTER_MIN_LENGTH_ERROR =
        "The Username is shorter than the allowed length of the user name. Min length - {0}, current length - {1}";

    /// <summary>
    /// Текст ошибки при попытке создать Username содержащий некорректный символ
    /// </summary>
    public const string USERNAME_INVALID_CHARACTER_ERROR =
        "The Username contains invalid characters. {0} not correct";
}