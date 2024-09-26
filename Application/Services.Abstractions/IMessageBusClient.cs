namespace Services.Abstractions;

/// <summary>
/// Интерфейс клиента шины сообщений
/// </summary>
public interface IMessageBusClient : IDisposable
{
    /// <summary>
    /// Отправить сообщение в шину
    /// </summary>
    /// <param name="message">Сообщение</param>
    /// <returns>Завершенная задача</returns>
    Task SendMessage(string message);
}