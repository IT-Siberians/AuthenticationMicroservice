using Services.Contracts;

namespace Services.Abstractions;

/// <summary>
/// Интерфейс сервиса управления пользователями.
/// </summary>
public interface IUserManagementService
{
    /// <summary>
    /// Возвращает список всех пользователей.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список моделей пользователей.</returns>
    public Task<IEnumerable<UserModel>> GetAllUsersAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Получает пользователя по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель пользователя или null, если пользователь не найден.</returns>
    public Task<UserModel?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Создает нового пользователя.
    /// </summary>
    /// <param name="model">Модель для создания пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Модель пользователя для чтения</returns>
    public Task<UserModel> CreateUserAsync(CreateUserModel model, CancellationToken cancellationToken);

    /// <summary>
    /// Изменение имени пользователя
    /// </summary>
    /// <param name="model">Модель для изменения имени пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Модель пользователя для чтения</returns>
    public Task<bool> ChangeFullNameAsync(ChangeUsernameModel model, CancellationToken cancellationToken);

    /// <summary>
    /// Изменяет пароль пользователя.
    /// </summary>
    /// <param name="model">Модель с данными для изменения пароля.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>True, если пароль успешно изменен; иначе false.</returns>
    public Task<bool> ChangePasswordAsync(ChangePasswordModel model, CancellationToken cancellationToken);

    /// <summary>
    /// Устанавливает новый Email для пользователя.
    /// </summary>
    /// <param name="model">Модель для установки Email.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель пользователя для чтения.</returns>
    public Task<UserModel?> SetUserEmailAsync(EmailConfirmationModel model, CancellationToken cancellationToken);

    /// <summary>
    /// Мягкое удаление пользователя по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>True, если пользователь успешно помечен как удаленный; иначе false.</returns>
    public Task<bool> DeleteUserSoftlyByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получает пользователя по логину (Email или имя пользователя).
    /// </summary>
    /// <param name="login">Логин пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель пользователя.</returns>
    public Task<UserModel?> GetUserByLoginAsync(string login, CancellationToken cancellationToken);
}