using Services.Contracts;

namespace Services.Implementations;

public interface IMessageBusPublisher
{
    public Task PublishNewEmail(PublicationOfEmailConfirmationModel publicationOfEmailConfirmationModel);
}