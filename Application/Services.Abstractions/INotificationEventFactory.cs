using Services.Contracts;

namespace Services.Abstractions;

/// <summary>
/// Интерфейс фабрики для создания событий уведомлений.
/// </summary>
public interface INotificationEventFactory
{
    /// <summary>
    /// Асинхронно создает событие уведомления для указанной модели пользователя.
    /// </summary>
    /// <param name="model">Модель пользователя, для которой создается уведомление.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Задача, представляющая объект события уведомления, который будет создан асинхронно.</returns>
    Task<INotificationEvent> CreateNotificationEventAsync(
        UserModel model,
        CancellationToken cancellationToken);
}