namespace Services.Abstractions;

/// <summary>
/// Интерфейс для уведомлений, которые могут быть асинхронно отправлены или обработаны.
/// </summary>
public interface INotificationEvent
{
    /// <summary>
    /// Асинхронно выполняет операцию отправки уведомления
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Задача, представляющая выполнение операции уведомления.</returns>
    Task NotifyAsync(CancellationToken cancellationToken);
}