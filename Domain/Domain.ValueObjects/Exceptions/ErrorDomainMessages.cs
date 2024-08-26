namespace Domain.ValueObjects.Exceptions;

/// <summary>
/// Класс содержащий доменные константы ошибок исключений доменного слоя
/// </summary>
internal static class ErrorDomainMessages
{
    /// <summary>
    /// Текст ошибки при попытке создать при пустом Username
    /// </summary>
    public const string USERNAME_EMPTY_STRING_ERROR = "Username cannot null or empty.";

    /// <summary>
    /// Текст ошибки при попытке создать Username длиннее установленного
    /// </summary>
    public const string USERNAME_LONGER_MAX_LENGTH_ERROR = "The Username is longer than the allowed length of the user name.";

    /// <summary>
    /// Текст ошибки при попытке создать Username короче установленного
    /// </summary>
    public const string USERNAME_SHORTER_MIN_LENGTH_ERROR = "The Username is shorter than the allowed length of the user name.";

    /// <summary>
    /// Текст ошибки при попытке создать Username с недопустимым символом
    /// </summary>
    public const string USERNAME_INVALID_CHARACTER_ERROR = "The Username contains invalid characters.";

    /// <summary>
    /// Текст ошибки при попытке создать пустом Email
    /// </summary>
    public const string EMAIL_EMPTY_ERROR = "Email cannot null or empty.";

    /// <summary>
    /// Текст ошибки при попытке создать Email длиннее установленного
    /// </summary>
    public const string EMAIL_LONGER_MAX_LENGTH_ERROR = "The Email is longer than the allowed length of the user name.";

    /// <summary>
    /// Текст ошибки при попытке создать Email в недопустимом формате
    /// </summary>
    public const string EMAIL_FORMAT_ERROR = "Invalid Email address format.";

    /// <summary>
    /// Текст ошибки при попытке создать пустом PasswordHash
    /// </summary>
    public const string PASSWORDHASH_EMPTY_ERROR = "PasswordHash cannot null or empty.";
}

