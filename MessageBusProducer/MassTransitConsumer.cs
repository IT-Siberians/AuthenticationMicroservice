using MassTransit;

namespace MessageBusClient;

    /// <summary>
    /// Обработчик сообщений для массовой передачи с использованием MassTransit.
    /// Реализует интерфейс <see cref="IConsumer{TMessage}"/>, обеспечивая обработку сообщений типа <typeparamref name="TMessage"/>.
    /// </summary>
    /// <typeparam name="TMessage">Тип сообщения, которое будет обрабатываться этим потребителем.</typeparam>
    public class MassTransitConsumer<TMessage>(
        IMessageProcessService<TMessage> service) : IConsumer<TMessage> 
        where TMessage : class
    {
        /// <summary>
        /// Метод для обработки поступившего сообщения.
        /// Этот метод вызывается при получении сообщения и передает его на обработку в соответствующий сервис.
        /// </summary>
        /// <param name="context">Контекст, содержащий сообщение, которое требуется обработать.</param>
        /// <returns>Асинхронная задача, которая завершится, когда обработка сообщения будет выполнена.</returns>
        public async Task Consume(ConsumeContext<TMessage> context)
        {
            await service.ProcessMessage(context.Message);
        }
    }