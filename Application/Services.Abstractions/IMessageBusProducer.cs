namespace Services.Abstractions;

/// <summary>
/// Интерфейс продюсера для публикации сообщений в очередь.
/// </summary>
public interface IMessageBusProducer
{
    /// <summary>
    /// Публикует данные в очередь.
    /// </summary>
    /// <typeparam name="T">Тип публикуемых данных.</typeparam>
    /// <param name="publishModel">Данные для публикации.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public Task PublishDataAsync<T>(T publishModel, CancellationToken cancellationToken);
}