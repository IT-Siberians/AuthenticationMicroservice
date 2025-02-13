namespace MessageBusClient;

public interface IMessageProcessService<in T>
{
    Task ProcessMessage(T message);
}