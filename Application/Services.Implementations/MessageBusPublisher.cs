using System.Text.Json;
using Services.Abstractions;
using Services.Contracts;

namespace Services.Implementations;

/// <summary>
/// Сервис публикующий данные в шину сообщений
/// </summary>
/// <param name="client">Клиент шины сообщений</param>
public class MessageBusPublisher(IMessageBusClient client) : IMessageBusPublisher
{
    /// <summary>
    /// Опубликовать подтверждение Email
    /// </summary>
    /// <param name="model">Модель подтверждения Email, для публикации</param>
    /// <returns>Завершенная задача</returns>
    public async Task PublishEmailConfirmation(PublishEmailConfirmationModel model)
    {
        var message = JsonSerializer.Serialize(model);
        await client.SendMessage(message);
        client.Dispose();
    }
}