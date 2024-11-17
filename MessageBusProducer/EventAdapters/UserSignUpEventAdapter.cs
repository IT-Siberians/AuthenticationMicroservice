using Otus.QueueDto.User;
using Services.Abstractions;

namespace MessageBusClient.EventAdapters;

/// <summary>
/// Адаптер для обработки события регистрации пользователя.
/// Реализует интерфейс <see cref="INotificationEvent"/>, который используется для уведомлений о событиях.
/// </summary>
public class UserSignUpEventAdapter(
    UserSignUpEvent userSignUpEvent,
    INotificationService notificationService) : INotificationEvent
{
    /// <summary>
    /// Метод для отправки уведомления о регистрации нового пользователя.
    /// Использует сервис уведомлений для уведомления о произошедшем событии регистрации.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
    /// <returns>Задача, представляющая асинхронную операцию уведомления.</returns>
    public async Task NotifyAsync(CancellationToken cancellationToken)
    {
        // Отправляем уведомление о регистрации нового пользователя
        await notificationService.NotifyChangeUserDataAsync(userSignUpEvent, cancellationToken);
    }
}