using AutoMapper;
using Common.Helpers.Domain.Enums;
using MessageBusClient.EventAdapters;
using Otus.QueueDto.User;
using Services.Abstractions;
using Services.Contracts;

namespace MessageBusClient;

/// <summary>
/// Фабрика для создания событий уведомлений, связанных с пользователем, на основе модели пользователя.
/// В зависимости от статуса аккаунта пользователя, фабрика создает соответствующие события для уведомлений.
/// </summary>
public class UserNotificationEventFactory(
    IMapper mapper,
    INotificationService notificationService) : INotificationEventFactory
{
    /// <summary>
    /// Создает соответствующее событие уведомления на основе модели пользователя и его статуса аккаунта.
    /// </summary>
    /// <param name="model">Модель пользователя, на основе которой будет создано событие уведомления.</param>
    /// <param name="cancellationToken">Токен отмены для управления асинхронными операциями.</param>
    /// <returns>Асинхронная задача, которая возвращает объект события уведомления, если оно было создано.</returns>
    /// <exception cref="ArgumentNullException">Если передана недопустимая модель пользователя.</exception>
    public async Task<INotificationEvent> CreateNotificationEventAsync(UserModel model, CancellationToken cancellationToken)
    {
        if (model == null) 
            throw new ArgumentNullException(nameof(model));
        switch (model.AccountStatus)
        {
            case AccountStatuses.UnconfirmedAccount:
                var userSignUpEvent = mapper.Map<UserSignUpEvent>(model);
                return new UserSignUpEventAdapter(userSignUpEvent, notificationService);

            case AccountStatuses.ConfirmedAccount:
                var emailChangedEvent = mapper.Map<EmailChangedEvent>(model);
                return new EmailChangedEventAdapter(emailChangedEvent, notificationService);

            case AccountStatuses.RestrictedAccount:
            default:
                return null;
        }
    }
}