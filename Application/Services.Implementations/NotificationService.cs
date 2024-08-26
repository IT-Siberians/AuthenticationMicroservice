using Domain.ValueObjects.ValueObjects;
using Repositories.Abstractions;
using Services.Abstractions;
using Services.Contracts;

namespace Services.Implementations;

/// <summary>
/// Сервис оповещений
/// </summary>
/// <param name="repository">Репозиторий пользователей</param>
public class NotificationService(IUserRepository repository) : INotificationService
{
    /// <summary>
    /// Создать запрос на установку почты
    /// </summary>
    /// <param name="mailConfirmationGenerationModel">Модель генерации подтверждения Email</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Возвращает true - запрос создан/ false - запрос не создан</returns>
    public async Task<bool> CreateSetEmailRequest(MailConfirmationGenerationModel mailConfirmationGenerationModel, CancellationToken cancellationToken)
    {
        var user = await repository.GetByIdAsync(mailConfirmationGenerationModel.Id, cancellationToken);
        if (user == null)
            return false;

        var newEmail = new Email(mailConfirmationGenerationModel.NewEmail); //здесь это нужно чтобы провалидировать эмейл
        //здесь будет логика отправки в почту

        return true;
    }
}