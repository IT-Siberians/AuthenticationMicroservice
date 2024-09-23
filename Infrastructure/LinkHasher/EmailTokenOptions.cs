namespace EmailTokenGenerator;

/// <summary>
/// Опции токена
/// </summary>
public class EmailTokenOptions
{
    /// <summary>
    /// Секретный ключ
    /// </summary>
    public required string Key { get; init; }

    /// <summary>
    /// Время жизни в минутах
    /// </summary>
    public required double LifetimeInMinutes { get; init; }
}