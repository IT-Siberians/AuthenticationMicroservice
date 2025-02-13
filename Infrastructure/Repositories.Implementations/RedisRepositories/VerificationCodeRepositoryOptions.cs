namespace Repositories.Implementations.RedisRepositories;

/// <summary>
/// Класс настроек для репозитория кодов подтверждения.
/// Используется для конфигурации времени жизни кода подтверждения в Redis.
/// </summary>
public class VerificationCodeRepositoryOptions
{
    /// <summary>
    /// Время жизни кода подтверждения в минутах.
    /// Указывает, сколько времени код будет храниться в Redis перед истечением срока.
    /// </summary>
    public double ExpiredTime { get; set; }
}