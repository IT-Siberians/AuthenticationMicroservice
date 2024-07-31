using Services.Contracts;

namespace Services.Implementations;
public class MessageBusPublisher : IMessageBusPublisher
{
    public Task PublishNewEmail(ChangeEmailDto changeEmailDto)
    {
        throw new NotImplementedException();
    }
}
