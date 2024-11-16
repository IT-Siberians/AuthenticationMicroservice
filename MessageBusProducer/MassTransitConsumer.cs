using MassTransit;

namespace MessageBusClient;

public class MassTransitConsumer<TMessage>(IMessageProcessService<TMessage> service) : IConsumer<TMessage> where TMessage : class
{
    public async Task Consume(ConsumeContext<TMessage> context)
    {
        await service.ProcessMessage(context.Message);
    }
}