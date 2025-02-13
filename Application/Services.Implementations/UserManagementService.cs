using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects.ValueObjects;
using Repositories.Abstractions;
using Services.Abstractions;
using Services.Contracts;
using Services.Implementations.Exceptions;

namespace Services.Implementations;
/// <summary>
/// Сервис для управления пользователями, включая операции создания, изменения, удаления пользователей и другие действия.
/// </summary>
/// <param name="repository">
/// Репозиторий, предоставляющий доступ к данным пользователей.
/// Используется для получения, добавления, обновления и удаления пользователей в базе данных.
/// </param>
/// <param name="notificationService">
/// Сервис уведомлений, который используется для отправки сообщений,
/// таких как запросы на подтверждение электронной почты после регистрации пользователя.
/// </param>
/// <param name="mapper">
/// Автомаппер, используется для преобразования сущностей модели данных в модели представления и наоборот,
/// чтобы можно было работать с различными слоями приложения.
/// </param>
/// <param name="hasher">
/// Шифровальщик паролей, используется для генерации безопасных хешей паролей пользователя,
/// чтобы их можно было безопасно хранить в базе данных.
/// </param>
public class UserManagementService(
IUserRepository repository,
    INotificationService notificationService,
    IMapper mapper,
    IPasswordHasher hasher) : IUserManagementService
{
    /// <summary>
    /// Получение списка всех пользователей для чтения.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены для асинхронных операций.</param>
    /// <returns>Перечисляемая коллекция моделей пользователей для чтения.</returns>
    public async Task<IEnumerable<UserModel>> GetAllUsersAsync(CancellationToken cancellationToken)
    {
        var users = await repository.GetAllAsync(cancellationToken);
        return mapper.Map<IEnumerable<UserModel>>(users);
    }

    /// <summary>
    /// Получить пользователя по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены для асинхронных операций.</param>
    /// <returns>Модель пользователя для чтения.</returns>
    public async Task<UserModel?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await repository.GetByIdAsync(id, cancellationToken);
        return mapper.Map<UserModel>(user);
    }

    /// <summary>
    /// Создание нового пользователя.
    /// </summary>
    /// <param name="model">Модель данных для создания пользователя.</param>
    /// <param name="cancellationToken">Токен отмены для асинхронных операций.</param>
    /// <exception cref="UserNotCreatedException">Выбрасывается, если пользователь не был создан.</exception>
    /// <returns>Модель созданного пользователя для чтения.</returns>
    public async Task<UserModel> CreateUserAsync(CreateUserModel model, CancellationToken cancellationToken)
    {
        var username = new Username(model.Username);
        var passwordHash = new PasswordHash(hasher.GenerateHashPassword(model.Password));
        var email = new Email(model.Email);
        var firstname = new FirstName(model.FirstName);
        var lastname = new LastName(model.LastName);

        var user = new User(username, passwordHash, email, firstname, lastname);
        var createdUser = await repository.AddAsync(user, cancellationToken)
                          ?? throw new UserNotCreatedException();

        var emailConfirmationModel = new EmailConfirmationModel(createdUser.Id, createdUser.Email.Value);

        await notificationService.SendEmailConfirmationAsync(emailConfirmationModel, cancellationToken);

        return mapper.Map<UserModel>(user);
    }

    /// <summary>
    /// Изменение имени и фамилии пользователя.
    /// </summary>
    /// <param name="model">Модель для изменения имени и фамилии пользователя.</param>
    /// <param name="cancellationToken">Токен отмены для асинхронных операций.</param>
    /// <returns>Возвращает true, если имя и фамилия были успешно изменены, иначе false.</returns>
    public async Task<bool> ChangeFullNameAsync(ChangeUsernameModel model, CancellationToken cancellationToken)
    {
        var user = await repository.GetByIdAsync(model.Id, cancellationToken);
        if (user is null)
            return false;

        var firstname = new FirstName(model.FirstName);
        var lastname = new LastName(model.LastName);

        user.ChangeFullname(firstname, lastname);

        var updatedUser = await repository.UpdateAsync(user, cancellationToken);
        return updatedUser.FirstName.Value == model.FirstName && updatedUser.LastName.Value == model.LastName;
    }

    /// <summary>
    /// Смена пароля пользователя.
    /// </summary>
    /// <param name="model">Модель для смены пароля.</param>
    /// <param name="cancellationToken">Токен отмены для асинхронных операций.</param>
    /// <returns>Возвращает true, если пароль был успешно изменен, иначе false.</returns>
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
    /// Смена электронной почты пользователя.
    /// </summary>
    /// <param name="model">Модель для смены email.</param>
    /// <param name="cancellationToken">Токен отмены для асинхронных операций.</param>
    /// <returns>Возвращает true, если email был успешно изменен, иначе false.</returns>
    public async Task<UserModel?> SetUserEmailAsync(EmailConfirmationModel model, CancellationToken cancellationToken)
    {
        var user = await repository.GetByIdAsync(model.Id, cancellationToken);
        if (user is null)
            return null;

        var email = new Email(model.NewEmail);

        user.ConfirmNewEmail(email);

        var updatedUser = await repository.UpdateAsync(user, cancellationToken);

        return mapper.Map<UserModel>(updatedUser);
    }

    /// <summary>
    /// Удаление пользователя по идентификатору (мягкое удаление).
    /// </summary>
    /// <param name="id">Идентификатор пользователя для удаления.</param>
    /// <param name="cancellationToken">Токен отмены для асинхронных операций.</param>
    /// <returns>Возвращает true, если пользователь был помечен как удаленный, иначе false.</returns>
    public async Task<bool> DeleteUserSoftlyByIdAsync(Guid id, CancellationToken cancellationToken)
        => await repository.DeleteSoftlyAsync(id, cancellationToken);

    /// <summary>
    /// Получение пользователя по имени пользователя или email.
    /// </summary>
    /// <param name="login">Имя пользователя или email.</param>
    /// <param name="cancellationToken">Токен отмены для асинхронных операций.</param>
    /// <returns>Модель пользователя для чтения.</returns>
    public async Task<UserModel?> GetUserByLoginAsync(string login, CancellationToken cancellationToken)
    {
        var user = await repository.GetUserByEmailAsync(login, cancellationToken)
                   ?? await repository.GetUserByUsernameAsync(login, cancellationToken);
        return mapper.Map<UserModel>(user);
    }
}