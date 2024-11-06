namespace MessageBusClient;

public interface IMessageBusProducer
{
    public Task PublishDataAsync<T>(T publishModel, CancellationToken cancellationToken);
}