using Domain.ValueObjects.ValueObjects;
using Otus.QueueDto.Notification;
using Repositories.Abstractions;
using Services.Abstractions;
using Services.Contracts;

namespace Services.Implementations;

/// <summary>
/// Сервис для отправки уведомлений пользователям.
/// </summary>
/// <param name="repository">Репозиторий пользователей.</param>
/// <param name="producer">Клиент для отправки сообщений в шину данных.</param>
/// <param name="verificationCodeService">Сервис для генерации и валидации кодов подтверждения.</param>
public class NotificationService(
    IUserRepository repository,
    IMessageBusProducer producer,
    IVerificationCodeService verificationCodeService) : INotificationService
{
    /// <summary>
    /// Отправляет пользователю письмо для подтверждения email-адреса.
    /// </summary>
    /// <param name="model">Модель данных, содержащая идентификатор пользователя и новый email.</param>
    /// <param name="cancellationToken">Токен отмены для управления асинхронной операцией.</param>
    /// <returns>
    /// Возвращает <c>true</c>, если письмо было успешно отправлено; иначе <c>false</c>.
    /// </returns>
    public async Task<bool> SendingEmailConfirmationAsync(EmailConfirmationModel model, CancellationToken cancellationToken)
    {
        var user = await repository.GetByIdAsync(model.Id, cancellationToken);
        if (user == null)
            return false;

        var newEmail = new Email(model.NewEmail);

        var code = await verificationCodeService.GenerateCodeAsync(user.Id, cancellationToken);

        const string culture = "ru";

        var emailPublishModel = new ConfirmationEmailEvent(newEmail.Value, user.Username.Value, new Uri("https://localhost"), code, culture);

        await producer.PublishDataAsync(emailPublishModel, cancellationToken);

        return true;
    }

    /// <summary>
    /// Отправляет уведомление об изменении данных пользователя.
    /// </summary>
    /// <param name="model">Модель данных пользователя, содержащая обновленные данные.</param>
    /// <param name="cancellationToken">Токен отмены для управления асинхронной операцией.</param>
    /// <returns>
    /// Возвращает <c>true</c>, если уведомление было успешно отправлено; иначе <c>false</c>.
    /// </returns>
    public async Task<bool> NotifyChangeUserDataAsync(UserModel model, CancellationToken cancellationToken)
    {
        if (model == null)
            return false;

        await producer.PublishDataAsync(model, cancellationToken);
        return true;
    }
}