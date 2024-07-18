using Services.Contracts;

namespace Services.Abstractions;

public interface INotificationPublisher
{
    Task PublishVerifyEmailLinkAsync(PublishVerifyEmailDto publishVerifyEmailDto);
}