using Otus.QueueDto.User;
using Services.Abstractions;
using Services.Contracts;

namespace MessageBusClient.Consumers;

/// <summary>
/// Сервис для обработки сообщений обновления пользователя.
/// Реализует интерфейс <see cref="IMessageProcessService{T}"/>, который предназначен для обработки событий.
/// </summary>
public class UpdateUserProcessService(IUserManagementService managementService) : IMessageProcessService<UpdateUserEvent>
{

    /// <summary>
    /// Метод для обработки события обновления пользователя.
    /// Разбивает полное имя пользователя на компоненты и передает их в сервис для обновления данных пользователя.
    /// </summary>
    /// <param name="message">Сообщение типа <see cref="UpdateUserEvent"/>, содержащее данные о пользователе.</param>
    /// <returns>Задача, представляющая асинхронную операцию обработки сообщения.</returns>
    public async Task ProcessMessage(UpdateUserEvent message)
    {
        var fullname = message.FullName.Split(' ');

        var model = new ChangeUsernameModel(message.Id, fullname[0], fullname[1]);

        await managementService.ChangeFullNameAsync(model, CancellationToken.None);
    }
}