using Services.Contracts;

namespace Services.Abstractions;

/// <summary>
/// Интерфейс валидации изменений пользователей
/// </summary>
public interface IUserValidationService
{
    /// <summary>
    /// Проверка свободно ли имя пользователя
    /// </summary>
    /// <param name="username">Проверяемое имя пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Возвращает true - имя пользователя свободно/ false - имя пользователя занято</returns>
    public Task<bool> IsAvailableUsernameAsync(string username, CancellationToken cancellationToken);

    /// <summary>
    /// Проверка свободен ли Email
    /// </summary>
    /// <param name="email">Проверяемый Email</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Возвращает true - Email свободен/ false - Email занят</returns>
    public Task<bool> IsAvailableEmailAsync(string email, CancellationToken cancellationToken);

    /// <summary>
    /// Проверка свободен ли Email, через модель установки Email
    /// </summary>
    /// <param name="model">Модель установки Email</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Возвращает true - Email свободен/ false - Email занят</returns>
    public Task<bool> IsAvailableEmailAsync(SetUserEmailModel model, CancellationToken cancellationToken);

    /// <summary>
    /// Валидация пароля
    /// </summary>
    /// <param name="validatePasswordModel">Модель валидации пароля</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Возвращает true - пароль верный/ false - некорректный пароль</returns>
    public Task<bool> ValidatePasswordAsync(ValidatePasswordModel validatePasswordModel,
        CancellationToken cancellationToken);

    /// <summary>
    /// Проверяет срок жизни ссылки
    /// </summary>
    /// <param name="expirationTime">Время истечения срока жизни ссылки</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Возвращает true - срок не истек/ false - срок истек</returns>
    public Task<bool> IsLinkExpiredAsync(DateTime expirationTime, CancellationToken cancellationToken);

}
