using Services.Contracts;

namespace Services.Abstractions;

/// <summary>
/// Интерфейс сервиса валидации изменений пользователей.
/// </summary>
public interface IUserValidationService
{
    /// <summary>
    /// Проверяет, свободно ли указанное имя пользователя.
    /// </summary>
    /// <param name="username">Имя пользователя для проверки.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>True, если имя пользователя свободно; иначе false.</returns>
    public Task<bool> IsAvailableUsernameAsync(string username, CancellationToken cancellationToken);

    /// <summary>
    /// Проверяет, свободен ли указанный Email.
    /// </summary>
    /// <param name="email">Email для проверки.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>True, если Email свободен; иначе false.</returns>
    public Task<bool> IsAvailableEmailAsync(string email, CancellationToken cancellationToken);

    /// <summary>
    /// Проверяет, соответствует ли указанный пароль хэшу пароля пользователя.
    /// </summary>
    /// <param name="validatePasswordModel">Модель с данными для проверки пароля.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>True, если пароль верный; иначе false.</returns>
    public Task<bool> ValidatePasswordAsync(ValidatePasswordModel validatePasswordModel, CancellationToken cancellationToken);

    /// <summary>
    /// Проверяет корректность кода верификации.
    /// </summary>
    /// <param name="requestId">Идентификатор запроса.</param>
    /// <param name="requestCode">Код верификации для проверки.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>True, если код корректен; иначе false.</returns>
    public Task<bool> ValidateVerificationCodeAsync(Guid requestId, int requestCode, CancellationToken cancellationToken);
}