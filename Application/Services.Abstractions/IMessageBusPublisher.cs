using Services.Contracts;

namespace Services.Implementations;

public interface IMessageBusPublisher
{
    Task PublishNewEmail(ChangeEmailDto changeEmailDto);
}