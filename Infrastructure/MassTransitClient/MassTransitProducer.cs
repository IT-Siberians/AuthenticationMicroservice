using MassTransit;
using Services.Abstractions;

namespace MassTransitClient;

/// <summary>
/// Продюсер в шину через MassTransit
/// </summary>
/// <param name="bus">Абстракция шины сообщений</param>
public class MassTransitProducer(IBus bus) : IMessageBusProducer
{
    /// <summary>
    /// Опубликовать данные
    /// </summary>
    /// <typeparam name="T">Обобщение отправляемых данных</typeparam>
    /// <param name="publishModel">Публикуемая модель</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task PublishDataAsync<T>(T publishModel, CancellationToken cancellationToken)
    {
        if (publishModel != null) await bus.Publish(publishModel, cancellationToken);
    }
}