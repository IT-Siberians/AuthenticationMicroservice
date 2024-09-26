using Domain.ValueObjects.ValueObjects;
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
    IMessageBusPublisher messageBusPublisher) : INotificationService
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
        var link = string.Empty; // здесь пока нет реализации ссылки

        var publishModel = new PublishEmailConfirmationModel()
        {
            Email = newEmail.Value,
            Link = link
        };
        await messageBusPublisher.PublishEmailConfirmation(publishModel);

        return true;
    }
}