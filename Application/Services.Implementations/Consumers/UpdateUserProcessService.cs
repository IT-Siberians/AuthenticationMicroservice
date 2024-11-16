using MessageBusClient;
using Otus.QueueDto.User;
using Services.Abstractions;
using Services.Contracts;

namespace Services.Implementations.Consumers;

public class UpdateUserProcessService(IUserManagementService managementService) : IMessageProcessService<UpdateUserEvent>
{
    public async Task ProcessMessage(UpdateUserEvent message)
    {
        var fullname = message.FullName.Split(' ');
        //Надо лм придумывать тут валидацию сообщения?
        var model = new ChangeUsernameModel(message.Id, fullname[0], fullname[1]);
        await managementService.ChangeFullNameAsync(model, CancellationToken.None);
    }
}