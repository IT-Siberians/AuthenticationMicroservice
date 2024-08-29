namespace Common.Helpers.EmailHelpers;

/// <summary>
/// Статический класс содержащий константы параметров исключений в Email
/// </summary>
public static class EmailConstants
{
    /// <summary>
    /// Максимальная длина Email
    /// </summary>
    public const int EMAIL_MAX_LENGTH = 255;

    /// <summary>
    /// Паттерн корректного Email
    /// </summary>
    public const string EMAIL_VALID_PATTERN = @"[.\-_a-z0-9]+@([a-z0-9][\-a-z0-9]+\.)+[a-z]{2,6}";
}