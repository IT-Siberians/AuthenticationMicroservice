using Otus.QueueDto.User;
using Services.Abstractions;

namespace MessageBusClient.EventAdapters;

/// <summary>
/// Адаптер для обработки события изменения email пользователя.
/// Реализует интерфейс <see cref="INotificationEvent"/>, который используется для уведомлений о событиях.
/// </summary>
public class EmailChangedEventAdapter(
    EmailChangedEvent emailChangedEvent,
    INotificationService notificationService) : INotificationEvent
{
    /// <summary>
    /// Метод для отправки уведомления об изменении email пользователя.
    /// Использует сервис уведомлений для уведомления о произошедшем изменении.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
    /// <returns>Задача, представляющая асинхронную операцию уведомления.</returns>
    public async Task NotifyAsync(CancellationToken cancellationToken)
    {
        await notificationService.NotifyChangeUserDataAsync(emailChangedEvent, cancellationToken);
    }
}