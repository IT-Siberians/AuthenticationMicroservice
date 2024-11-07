using Domain.ValueObjects.ValueObjects;
using MessageBusClient;
using Otus.QueueDto.Notification;
using Repositories.Abstractions;
using Services.Abstractions;
using Services.Contracts;

namespace Services.Implementations;

/// <summary>
/// Сервис оповещений
/// </summary>
/// <param name="repository">Репозиторий пользователей</param>
public class NotificationService(
    IUserRepository repository,
    IMessageBusProducer producer,
    IVerificationCodeService verificationCodeService) : INotificationService
{
    public async Task<bool> SendingEmailConfirmationAsync(EmailConfirmationModel model, CancellationToken cancellationToken)
    {
        var user = await repository.GetByIdAsync(model.Id, cancellationToken);
        if (user == null)
            return false;

        var newEmail = new Email(model.NewEmail);

        var code = await verificationCodeService.GenerateCodeAsync(user.Id, cancellationToken);
        
        const string culture = "ru";

        var emailPublishModel = new ConfirmationEmailEvent(newEmail.Value, user.Username.Value, new Uri($"http://localhost/{code}"), culture);// исправить
        await producer.PublishDataAsync(emailPublishModel, cancellationToken);

        return true;
    }

    public async Task<bool> NotifyChangeUserDataAsync(UserModel model, CancellationToken cancellationToken)
    {
        if (model == null)
            return false;

        await producer.PublishDataAsync(model, cancellationToken);
        return true;
    }
}