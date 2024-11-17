namespace Services.Abstractions;

/// <summary>
/// Интерфейс сервиса для работы с кодами верификации.
/// </summary>
public interface IVerificationCodeService
{
    /// <summary>
    /// Генерирует новый код верификации для указанного пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя, для которого генерируется код.</param>
    /// <param name="cancellationToken">Токен отмены операции, который может быть использован для прерывания операции.</param>
    /// <returns>Сгенерированный код верификации.</returns>
    public Task<ushort> GenerateCodeAsync(Guid userId, CancellationToken cancellationToken);

    /// <summary>
    /// Получает и проверяет код верификации для указанного пользователя.
    /// </summary>
    /// <param name="id">Идентификатор пользователя, для которого проверяется код.</param>
    /// <param name="code">Код верификации, который нужно проверить.</param>
    /// <param name="cancellationToken">Токен отмены операции, который может быть использован для прерывания операции.</param>
    /// <returns>Значение <c>true</c>, если код верификации валиден для указанного пользователя, иначе <c>false</c>.</returns>
    public Task<bool> ValidateCodeByUserIdAsync(Guid id, ushort code, CancellationToken cancellationToken);
}