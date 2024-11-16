using Services.Contracts;

namespace Services.Abstractions;

/// <summary>
/// Интерфейс сервиса оповещений.
/// </summary>
public interface INotificationService
{
    /// <summary>
    /// Отправляет запрос на подтверждение Email.
    /// </summary>
    /// <param name="model">Модель с данными для подтверждения Email.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>True, если запрос создан; иначе false.</returns>
    public Task<bool> SendingEmailConfirmationAsync(EmailConfirmationModel model, CancellationToken cancellationToken);

    /// <summary>
    /// Уведомляет об изменении данных пользователя.
    /// </summary>
    /// <param name="model">Модель данных пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>True, если уведомление отправлено успешно; иначе false.</returns>
    public Task<bool> NotifyChangeUserDataAsync(UserModel model, CancellationToken cancellationToken);
}