namespace Common.Helpers.UsernameHelpers;

/// <summary>
/// Статический класс содержащий константы параметров исключений в Username
/// </summary>
public static class UsernameConstants
{
    /// <summary>
    /// Минимальная длина Username
    /// </summary>
    public const int USERNAME_MIN_LENGTH = 3;

    /// <summary>
    /// Максимальная длина Username
    /// </summary>
    public const int USERNAME_MAX_LENGTH = 30;

    /// <summary>
    /// Паттерн корректного Username
    /// </summary>
    public const string USERNAME_VALID_PATTERN = "(^[a-zA-Z_-]+$)";
}