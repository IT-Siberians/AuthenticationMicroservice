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
    IMessageBusProducer producer) : INotificationService
{
    /// <summary>
    /// Создать запрос на установку почты
    /// </summary>
    /// <param name="model">Модель генерации подтверждения Email</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Возвращает true - запрос создан/ false - запрос не создан</returns>
    public async Task<bool> SendingEmailConfirmationAsync(EmailConfirmationModel model, CancellationToken cancellationToken)
    {
        var user = await repository.GetByIdAsync(model.Id, cancellationToken);
        if (user == null)
            return false;

        var newEmail = new Email(model.NewEmail);

        var link = new Uri("http://localhost:7263/api/v1/Confirms/ConfirmEmail");
        const string culture = "ru";

        var emailPublishModel = new ConfirmationEmailEvent(newEmail.Value, user.Username.Value, link, culture);
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