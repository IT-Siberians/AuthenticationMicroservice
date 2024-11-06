using Services.Contracts;

namespace Services.Abstractions;

/// <summary>
/// Интерфейс сервиса оповещений
/// </summary>
public interface INotificationService
{
    /// <summary>
    /// Создать запрос на установку почты
    /// </summary>
    /// <param name="model">Модель генерации подтверждения Email</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Возвращает true - запрос создан/ false - запрос не создан</returns>
    public Task<bool> SendingEmailConfirmationAsync(EmailConfirmationModel model,
        CancellationToken cancellationToken);

    public Task<bool> NotifyChangeUserDataAsync(UserModel model,
        CancellationToken cancellationToken);
}