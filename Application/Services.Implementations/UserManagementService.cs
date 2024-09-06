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
    /// <param name="createUserModel">Модель для создания пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <exception cref="UserNotCreatedException">Репозиторий не смог создать пользователя</exception>
    /// <returns>Модель пользователя для чтения</returns>
    public async Task<UserModel> CreateUserAsync(CreateUserModel createUserModel, CancellationToken cancellationToken)
    {
        var username = new Username(createUserModel.Username);
        var passwordHash = new PasswordHash(hasher.GenerateHashPassword(createUserModel.Password));
        var email = new Email(createUserModel.Email);

        var user = new User(username, passwordHash, email);
        var createdUser = await repository.AddAsync(user, cancellationToken);
        if (createdUser == null)
            throw new UserNotCreatedException();

        var mailConfirmationGenerationModel = new MailConfirmationGenerationModel()
        {
            Id = createdUser.Id,
            NewEmail = createdUser.Email.Value
        };

        await notificationService.CreateSetEmailRequest(mailConfirmationGenerationModel, cancellationToken);

        return mapper.Map<UserModel>(user);
    }

    /// <summary>
    /// Изменение имени пользователя(никнейм)
    /// </summary>
    /// <param name="changeUsernameModel">Модель для изменения имени пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Модель пользователя для чтения</returns>
    public async Task<bool> ChangeUsernameAsync(ChangeUsernameModel changeUsernameModel, CancellationToken cancellationToken)
    {
        var user = await repository.GetByIdAsync(changeUsernameModel.Id, cancellationToken);
        if (user is null)
            return false;

        user.ChangeUsername(changeUsernameModel.NewUsername);

        var updatedUser = await repository.UpdateAsync(user, cancellationToken);
        return updatedUser.Username.Value == changeUsernameModel.NewUsername;
    }

    /// <summary>
    /// Смена пароля пользователя
    /// </summary>
    /// <param name="changePasswordModel">Модель смены пароля</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Возвращает true - смена пароля прошла успешно/ false - пароль не изменен</returns>
    public async Task<bool> ChangePasswordAsync(ChangePasswordModel changePasswordModel, CancellationToken cancellationToken)
    {
        var user = await repository.GetByIdAsync(changePasswordModel.Id, cancellationToken);
        if (user is null)
            return false;

        var newPassword = hasher.GenerateHashPassword(changePasswordModel.NewPassword);
        user.ChangePasswordHash(newPassword);

        var updatedUser = await repository.UpdateAsync(user, cancellationToken);
        return updatedUser.PasswordHash.Value == newPassword;
    }

    /// <summary>
    /// Смена имени пользователя(никнейм)
    /// </summary>
    /// <param name="setUserEmailModel">Модель смены имени пользователя(никнейма)</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Возвращает true - смена имени пользователя прошла успешно/ false - имя пользователя не изменено</returns>
    public async Task<bool> SetUserEmailAsync(SetUserEmailModel setUserEmailModel, CancellationToken cancellationToken)
    {
        var user = await repository.GetByIdAsync(setUserEmailModel.Id, cancellationToken);
        if (user is null)
            return false;

        user.ConfirmNewEmail(setUserEmailModel.NewEmail);

        var updatedUser = await repository.UpdateAsync(user, cancellationToken);
        return updatedUser.Email.Value == setUserEmailModel.NewEmail;
    }

    /// <summary>
    /// Удалить пользователя по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Возвращает true - пользователь помечен как удаленный/ false - пользователь не удален</returns>
    public async Task<bool> DeleteUserSoftlyByIdAsync(Guid id, CancellationToken cancellationToken)
        => await repository.DeleteSoftlyAsync(id, cancellationToken);
}