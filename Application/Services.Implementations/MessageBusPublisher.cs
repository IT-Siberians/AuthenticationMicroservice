using Services.Contracts;

namespace Services.Implementations;
public class MessageBusPublisher : IMessageBusPublisher
{
    public Task PublishNewEmail(PublicationOfEmailConfirmationDto publicationOfEmailConfirmationDto)
    {
        throw new NotImplementedException();
    }
}
