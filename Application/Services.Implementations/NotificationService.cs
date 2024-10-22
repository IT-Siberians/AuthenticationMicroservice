using Domain.ValueObjects.ValueObjects;
using Otus.QueueDto.Notification;
using Repositories.Abstractions;
using Services.Abstractions;
using Services.Contracts;

namespace Services.Implementations;

/// <summary>
/// Сервис оповещений
/// </summary>
/// <param name="repository">Репозиторий пользователей</param>
/// <param name="messageBusProducer">Продюсер в шину сообщений</param>
public class NotificationService(
    IUserRepository repository,
    IMessageBusProducer messageBusProducer) : INotificationService
{
    /// <summary>
    /// Создать запрос на установку почты
    /// </summary>
    /// <param name="model">Модель генерации подтверждения Email</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Возвращает true - запрос создан/ false - запрос не создан</returns>
    public async Task<bool> CreateSetEmailRequest(MailConfirmationGenerationModel model, CancellationToken cancellationToken)
    {
        var user = await repository.GetByIdAsync(model.Id, cancellationToken);
        if (user == null)
            return false;

        var newEmail = new Email(model.NewEmail);
        var link = new Uri("https://localhost"); // здесь пока нет реализации ссылки

        const string culture = "ru"; // надо придумать как прошить культуру

        var publishModel = new ConfirmationEmailEvent(newEmail.Value, user.Username.Value, link, culture);
        await messageBusProducer.PublishDataAsync(publishModel, cancellationToken);

        return true;
    }
}