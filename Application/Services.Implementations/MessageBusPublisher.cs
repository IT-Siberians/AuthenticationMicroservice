using Services.Contracts;

namespace Services.Implementations;
public class MessageBusPublisher : IMessageBusPublisher
{
    public Task PublishNewEmail(PublicationOfEmailConfirmationModel publicationOfEmailConfirmationModel)
    {
        return Task.CompletedTask;
    }
}
