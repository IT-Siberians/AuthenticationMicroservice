namespace AsyncDataServices;

/// <summary>
/// Опции клиента сообщений
/// </summary>
public class MessageBusClientOptions
{
    /// <summary>
    /// Название хоста rmq
    /// </summary>
    public required string RabbitMqHost { get; init; }

    /// <summary>
    /// Порт хоста rmq
    /// </summary>
    public required int RabbitMqPort { get; init; }

    /// <summary>
    /// Название обменника
    /// </summary>
    public required string ConfirmEmailExchange { get; init; }
}