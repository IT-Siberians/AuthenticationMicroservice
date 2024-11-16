namespace Services.Abstractions;

/// <summary>
/// Интерфейс сервиса для работы с кодами верификации.
/// </summary>
public interface IVerificationCodeService
{
    /// <summary>
    /// Генерирует новый код верификации для указанного пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Сгенерированный код верификации.</returns>
    public Task<ushort> GenerateCodeAsync(Guid userId, CancellationToken cancellationToken);

    /// <summary>
    /// Получает код верификации для указанного пользователя.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="code"></param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель кода верификации или null, если код не найден.</returns>
    public Task<bool> ValidateCodeByUserIdAsync(Guid id, ushort code,
        CancellationToken cancellationToken);
}