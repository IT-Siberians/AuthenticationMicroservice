using Domain.ValueObjects.ValueObjects;
using Repositories.Abstractions;
using Services.Abstractions;
using Services.Contracts;

namespace Services.Implementations;

/// <summary>
/// Сервис оповещений
/// </summary>
/// <param name="repository">Репозиторий пользователей</param>
public class NotificationService(IUserRepository repository,
    ILinkGeneratorService linkGeneratorService) : INotificationService
{
    /// <summary>
    /// Создать запрос на установку почты
    /// </summary>
    /// <param name="model">Модель генерации подтверждения Email</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Возвращает true - запрос создан/ false - запрос не создан</returns>
    public async Task<bool> CreateSetEmailRequestAsync(MailConfirmationGenerationModel model, CancellationToken cancellationToken)
    {
        var newEmail = new Email(model.NewEmail);

        var confirmationLink = await linkGeneratorService.GenerateLinkAsync(model, cancellationToken);

        if (string.IsNullOrWhiteSpace(confirmationLink))
            return false;//это нужно пока не прикрутил паблишер, для сохранения логики, будет буль возвращаться после паблиша

        var emailConfirmModel = new PublishEmailConfirmModel()
        {
            Id = model.Id,
            NewEmail = newEmail.Value,
            Link = confirmationLink
        };

        if (emailConfirmModel is null)
            return false;//это нужно пока не прикрутил паблишер, для сохранения логики, будет буль возвращаться после паблиша

        //здесь осталось опубликовать в шину

        return true;
    }
}