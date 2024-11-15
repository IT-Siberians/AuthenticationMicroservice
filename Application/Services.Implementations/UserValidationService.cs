using Repositories.Abstractions;
using Services.Abstractions;
using Services.Contracts;

namespace Services.Implementations;

/// <summary>
/// Сервис валидации изменений пользователей
/// </summary>
/// <param name="repository">
/// Репозиторий, предоставляющий доступ к данным пользователей.
/// Используется для получения, добавления, обновления и удаления пользователей в базе данных.
/// </param>
/// <param name="hasher">
/// Шифровальщик паролей, используется для генерации безопасных хешей паролей пользователя,
/// чтобы их можно было безопасно хранить в базе данных.
/// </param>
public class UserValidationService(
    IUserRepository repository,
    IPasswordHasher hasher) : IUserValidationService
{
    /// <summary>
    /// Проверка свободен ли никнейм
    /// </summary>
    /// <param name="username">Проверяемый никнейм</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Возвращает true - никнейм свободен/ false - никнейм занят</returns>
    public async Task<bool> IsAvailableUsernameAsync(string username, CancellationToken cancellationToken)
    {
        return await repository.GetUserByUsernameAsync(username, cancellationToken) == null;
    }

    /// <summary>
    /// Проверка свободен ли Email
    /// </summary>
    /// <param name="email">Проверяемый Email</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Возвращает true - Email свободен/ false - Email занят</returns>
    public async Task<bool> IsAvailableEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await repository.GetUserByEmailAsync(email, cancellationToken) == null;
    }

    /// <summary>
    /// Валидация пароля
    /// </summary>
    /// <param name="validatePasswordModel">Модель валидации пароля</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Возвращает true - пароль верный/ false - некорректный пароль</returns>
    public async Task<bool> ValidatePasswordAsync(ValidatePasswordModel validatePasswordModel, CancellationToken cancellationToken)
    {
        var user = await repository.GetByIdAsync(validatePasswordModel.Id, cancellationToken);
        return user != null && hasher.VerifyHashedPassword(validatePasswordModel.Password, user.PasswordHash.Value);
    }

    /// <summary>
    /// Проверяет срок жизни ссылки
    /// </summary>
    /// <param name="createdTime">Время создания ссылки</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Возвращает true - срок не истек/ false - срок истек</returns>
    public Task<bool> IsLinkExpiredAsync(DateTime createdTime, CancellationToken cancellationToken)
    {
        var time = DateTime.Now - createdTime;
        return Task.FromResult(time.TotalMinutes > 15);
    }
}