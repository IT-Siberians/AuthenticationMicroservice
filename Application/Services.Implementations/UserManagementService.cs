using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects.ValueObjects;
using Repositories.Abstractions;
using Services.Abstractions;
using Services.Contracts;
using Services.Implementations.Exceptions;

namespace Services.Implementations;

/// <summary>
/// Интерфейс менеджера пользователей
/// </summary>
/// <param name="notificationService">Сервис оповещений</param>
/// <param name="mapper">Автомаппер</param>
/// <param name="repository">Репозиторий пользователей</param>
/// <param name="hasher">Шифровальщик пароля</param>
public class UserManagementService(
    IUserRepository repository,
    INotificationService notificationService,
    IMapper mapper,
    IPasswordHasher hasher) : IUserManagementService
{
    /// <summary>
    /// Получить весь список моделей пользователя для чтения
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Перечисляемая коллекция моделей пользователя для чтения</returns>
    public async Task<IEnumerable<UserModel>> GetAllUsersAsync(CancellationToken cancellationToken)
    {
        var users = await repository.GetAllAsync(cancellationToken);
        return mapper.Map<IEnumerable<UserModel>>(users);
    }

    /// <summary>
    /// Получить пользователя по его идентификатору
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Модель пользователя для чтения</returns>
    public async Task<UserModel?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await repository.GetByIdAsync(id, cancellationToken);
        return mapper.Map<UserModel>(user);
    }

    /// <summary>
    /// Создать пользователя
    /// </summary>
    /// <param name="model">Модель для создания пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <exception cref="UserNotCreatedException">Репозиторий не смог создать пользователя</exception>
    /// <returns>Модель пользователя для чтения</returns>
    public async Task<UserModel> CreateUserAsync(CreateUserModel model, CancellationToken cancellationToken)
    {
        var username = new Username(model.Username);
        var passwordHash = new PasswordHash(hasher.GenerateHashPassword(model.Password));
        var email = new Email(model.Email);

        var user = new User(username, passwordHash, email);
        var createdUser = await repository.AddAsync(user, cancellationToken)
                          ?? throw new UserNotCreatedException();

        var emailConfirmationModel =
            new EmailConfirmationModel(createdUser.Id, createdUser.Email.Value);

        await notificationService.SendingEmailConfirmationAsync(emailConfirmationModel, cancellationToken);

        var resultModel = mapper.Map<UserModel>(user);
        await notificationService.NotifyChangeUserDataAsync(resultModel, cancellationToken);

        return resultModel;
    }

    /// <summary>
    /// Изменение имени пользователя(никнейм)
    /// </summary>
    /// <param name="model">Модель для изменения имени пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Модель пользователя для чтения</returns>
    public async Task<bool> ChangeUsernameAsync(ChangeUsernameModel model, CancellationToken cancellationToken)
    {
        var user = await repository.GetByIdAsync(model.Id, cancellationToken);
        if (user is null)
            return false;

        user.ChangeUsername(model.NewUsername);

        var updatedUser = await repository.UpdateAsync(user, cancellationToken);

        var userModel = mapper.Map<UserModel>(updatedUser);
        await notificationService.NotifyChangeUserDataAsync(userModel, cancellationToken);

        return updatedUser.Username.Value == model.NewUsername;
    }

    /// <summary>
    /// Смена пароля пользователя
    /// </summary>
    /// <param name="model">Модель смены пароля</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Возвращает true - смена пароля прошла успешно/ false - пароль не изменен</returns>
    public async Task<bool> ChangePasswordAsync(ChangePasswordModel model, CancellationToken cancellationToken)
    {
        var user = await repository.GetByIdAsync(model.Id, cancellationToken);
        if (user is null)
            return false;

        var newPassword = hasher.GenerateHashPassword(model.NewPassword);
        user.ChangePasswordHash(newPassword);

        var updatedUser = await repository.UpdateAsync(user, cancellationToken);
        return updatedUser.PasswordHash.Value == newPassword;
    }

    /// <summary>
    /// Смена Email
    /// </summary>
    /// <param name="model">Модель смены Email</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Возвращает true - смена Email прошла успешно/ false - Email не изменено</returns>
    public async Task<bool> SetUserEmailAsync(EmailConfirmationModel model, CancellationToken cancellationToken)
    {
        var user = await repository.GetByIdAsync(model.Id, cancellationToken);
        if (user is null)
            return false;

        user.ConfirmNewEmail(model.NewEmail);

        var updatedUser = await repository.UpdateAsync(user, cancellationToken);

        var userModel = mapper.Map<UserModel>(updatedUser);
        await notificationService.NotifyChangeUserDataAsync(userModel, cancellationToken);

        return updatedUser.Email.Value == model.NewEmail;
    }

    /// <summary>
    /// Удалить пользователя по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Возвращает true - пользователь помечен как удаленный/ false - пользователь не удален</returns>
    public async Task<bool> DeleteUserSoftlyByIdAsync(Guid id, CancellationToken cancellationToken)
        => await repository.DeleteSoftlyAsync(id, cancellationToken);

    public async Task<UserModel> GetUserByLoginAsync(string login, CancellationToken cancellationToken)
    {
        var user = await repository.GetUserByEmailAsync(login, cancellationToken)
                   ?? await repository.GetUserByUsernameAsync(login, cancellationToken);
        return mapper.Map<UserModel>(user);
    }
}