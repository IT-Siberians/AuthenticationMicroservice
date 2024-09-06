namespace Domain.Helpers.PasswordHashHelpers;

/// <summary>
/// Статический класс содержащий константы ошибок исключений в PasswordHash в доменном слое
/// </summary>
public static class PasswordHashDomainMessages
{
    /// <summary>
    /// Текст ошибки при попытке создать пустом PasswordHash
    /// </summary>
    public const string PASSWORDHASH_EMPTY_ERROR =
        "PasswordHash cannot null or empty. {0} not correct.";

    /// <summary>
    /// Текст ошибки при попытке создать PasswordHash длиннее установленного
    /// </summary>
    public const string PASSWORDHASH_MAX_LENGTH_ERROR =
        "The Password Hash is longer than the allowed length of the user name. Max length - {0}, current length - {1}";
}
