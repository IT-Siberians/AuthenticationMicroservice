using Services.Contracts;

namespace Services.Abstractions;

/// <summary>
/// Интерфейс публикующий данные в шину сообщений
/// </summary>
public interface IMessageBusPublisher
{
    /// <summary>
    /// Опубликовать подтверждение Email
    /// </summary>
    /// <param name="model">Модель подтверждения Email, для публикации</param>
    /// <returns>Завершенная задача</returns>
    Task PublishEmailConfirmation(PublishEmailConfirmationModel model);
}