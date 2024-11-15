using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects.ValueObjects;
using Repositories.Abstractions;
using Services.Abstractions;
using Services.Contracts;
using Services.Implementations.Exceptions;

namespace Services.Implementations;
/// <summary>
/// Сервис для управления пользователями.
/// </summary>
/// <param name="repository">Репозиторий пользователей.</param>
/// <param name="notificationService">Сервис оповещений.</param>
/// <param name="mapper">Автомаппер для преобразования данных.</param>
/// <param name="hasher">Интерфейс для работы с хешированием паролей.</param>
public class UserManagementService(
    IUserRepository repository,
    INotificationService notificationService,
    IMapper mapper,
    IPasswordHasher hasher) : IUserManagementService
{
    /// <summary>
    /// Получить список всех пользователей.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Перечисляемая коллекция моделей пользователей.</returns>
    public async Task<IEnumerable<UserModel>> GetAllUsersAsync(CancellationToken cancellationToken)
    {
        var users = await repository.GetAllAsync(cancellationToken);
        return mapper.Map<IEnumerable<UserModel>>(users);
    }

    /// <summary>
    /// Получить пользователя по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель пользователя или null, если пользователь не найден.</returns>
    public async Task<UserModel?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await repository.GetByIdAsync(id, cancellationToken);
        return mapper.Map<UserModel>(user);
    }

    /// <summary>
    /// Создать нового пользователя.
    /// </summary>
    /// <param name="model">Модель для создания пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <exception cref="UserNotCreatedException">Выбрасывается, если создание пользователя не удалось.</exception>
    /// <returns>Созданная модель пользователя.</returns>
    public async Task<UserModel> CreateUserAsync(CreateUserModel model, CancellationToken cancellationToken)
    {
        var username = new Username(model.Username);
        var passwordHash = new PasswordHash(hasher.GenerateHashPassword(model.Password));
        var email = new Email(model.Email);

        var user = new User(username, passwordHash, email);
        var createdUser = await repository.AddAsync(user, cancellationToken)
                          ?? throw new UserNotCreatedException();

        var emailConfirmationModel = new EmailConfirmationModel(createdUser.Id, createdUser.Email.Value);

        await notificationService.SendingEmailConfirmationAsync(emailConfirmationModel, cancellationToken);

        var resultModel = mapper.Map<UserModel>(user);

        await notificationService.NotifyChangeUserDataAsync(resultModel, cancellationToken);

        return resultModel;
    }

    /// <summary>
    /// Изменить имя пользователя.
    /// </summary>
    /// <param name="model">Модель с данными для изменения имени.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>True, если имя успешно изменено; иначе false.</returns>
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
    /// Сменить пароль пользователя.
    /// </summary>
    /// <param name="model">Модель с данными для смены пароля.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>True, если пароль успешно изменен; иначе false.</returns>
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
    /// Сменить email пользователя.
    /// </summary>
    /// <param name="model">Модель с данными для смены email.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>True, если email успешно изменен; иначе false.</returns>
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
    /// Удалить пользователя (мягкое удаление).
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>True, если пользователь успешно удален; иначе false.</returns>
    public async Task<bool> DeleteUserSoftlyByIdAsync(Guid id, CancellationToken cancellationToken)
        => await repository.DeleteSoftlyAsync(id, cancellationToken);

    /// <summary>
    /// Найти пользователя по логину (email или username).
    /// </summary>
    /// <param name="login">Логин пользователя (email или username).</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель пользователя.</returns>
    public async Task<UserModel> GetUserByLoginAsync(string login, CancellationToken cancellationToken)
    {
        var user = await repository.GetUserByEmailAsync(login, cancellationToken)
                   ?? await repository.GetUserByUsernameAsync(login, cancellationToken);
        return mapper.Map<UserModel>(user);
    }
}