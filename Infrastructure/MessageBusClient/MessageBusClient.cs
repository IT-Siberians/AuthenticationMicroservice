using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using Services.Abstractions;

namespace AsyncDataServices;

/// <summary>
/// Сервис клиент шины сообщений
/// </summary>
public class MessageBusClient : IMessageBusClient
{
    private readonly MessageBusClientOptions _options;
    private readonly IConnection _connection;
    private readonly IModel _chanel;

    /// <summary>
    /// Создание клиента шины сообщений с опциями
    /// </summary>
    /// <param name="options">Опции клиента шины сообщений</param>
    public MessageBusClient(IOptions<MessageBusClientOptions> options)
    {
        _options = options.Value;
        var factory = new ConnectionFactory()
        {
            HostName = _options.RabbitMqHost,
            Port = _options.RabbitMqPort
        };
        _connection = factory.CreateConnection();
        _chanel = _connection.CreateModel();
        _chanel.ExchangeDeclare(
            exchange: _options.ConfirmEmailExchange,
            type: ExchangeType.Fanout);
    }

    /// <summary>
    /// Отправить сообщение в шину
    /// </summary>
    /// <param name="message">Сообщение</param>
    /// <returns>Завершенная задача</returns>
    public async Task SendMessage(string message)
    {
        await Task.Run(
            () =>
            {
                var body = Encoding.UTF8.GetBytes(message);

                _chanel.BasicPublish(
                    exchange: _options.ConfirmEmailExchange,
                    routingKey: "",
                    basicProperties: null,
                    body: body);
            });
    }

    /// <summary>
    /// Закрыть клиента
    /// </summary>
    public void Dispose()
    {
        if (_chanel.IsClosed) return;
        _chanel.Close();
        _connection.Close();
    }
}