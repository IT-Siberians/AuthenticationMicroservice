namespace Services.Abstractions;

/// <summary>
/// Интерфейс продюсера в шину сообщений
/// </summary>
public interface IMessageBusProducer
{
    /// <summary>
    /// Опубликовать данные
    /// </summary>
    /// <typeparam name="T">Обобщение отправляемых данных</typeparam>
    /// <param name="publishModel">Публикуемая модель</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Асинхронная задача</returns>
    Task PublishData<T>(T publishModel, CancellationToken cancellationToken);
}
