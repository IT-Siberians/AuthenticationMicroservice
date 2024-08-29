using Services.Contracts;

namespace Services.Abstractions;

/// <summary>
/// Интерфейс менеджера пользователей
/// </summary>
public interface IUserManagementService
{
    /// <summary>
    /// Получить весь список моделей пользователя для чтения
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Перечисляемая коллекция моделей пользователя для чтения</returns>
    public Task<IEnumerable<UserModel>> GetAllUsersAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Получить пользователя по его идентификатору
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Модель пользователя для чтения</returns>
    public Task<UserModel?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Создать пользователя
    /// </summary>
    /// <param name="createUserModel">Модель для создания пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Модель пользователя для чтения</returns>
    public Task<UserModel> CreateUserAsync(CreateUserModel createUserModel, CancellationToken cancellationToken);

    /// <summary>
    /// Изменение имени пользователя(никнейм)
    /// </summary>
    /// <param name="changeUsernameModel">Модель для изменения имени пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Модель пользователя для чтения</returns>
    public Task<bool> ChangeUsernameAsync(ChangeUsernameModel changeUsernameModel, CancellationToken cancellationToken);

    /// <summary>
    /// Смена пароля пользователя
    /// </summary>
    /// <param name="changePasswordModel">Модель смены пароля</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Возвращает true - смена пароля прошла успешно/ false - пароль не изменен</returns>
    public Task<bool> ChangePasswordAsync(ChangePasswordModel changePasswordModel, CancellationToken cancellationToken);

    /// <summary>
    /// Смена имени пользователя(никнейм)
    /// </summary>
    /// <param name="setUserEmailModel">Модель смены имени пользователя(никнейма)</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Возвращает true - смена имени пользователя прошла успешно/ false - имя пользователя не изменено</returns>
    public Task<bool> SetUserEmailAsync(SetUserEmailModel setUserEmailModel, CancellationToken cancellationToken);

    /// <summary>
    /// Удалить пользователя по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Возвращает true - пользователь помечен как удаленный/ false - пользователь не удален</returns>
    public Task<bool> SoftDeleteUserByIdAsync(Guid id, CancellationToken cancellationToken);
}