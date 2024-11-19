using Repositories.Abstractions;
using Services.Abstractions;
using Services.Contracts;

namespace Services.Implementations;

/// <summary>
/// Сервис валидации изменений пользователей.
/// </summary>
/// <param name="userRepository">
/// Репозиторий, предоставляющий доступ к данным пользователей.
/// Используется для получения, добавления, обновления и удаления пользователей в базе данных.
/// </param>
/// <param name="hasher">
/// Шифровальщик паролей, используется для генерации безопасных хешей паролей пользователя,
/// чтобы их можно было безопасно хранить в базе данных.
/// </param>
/// <param name="verificationCodeService">
/// Сервис, предоставляющий функциональность для генерации и валидации кодов подтверждения.
/// Используется для проверки правильности введённых кодов подтверждения пользователя.
/// </param>
public class UserValidationService(
IUserRepository userRepository,
    IPasswordHasher hasher,
    IVerificationCodeService verificationCodeService) : IUserValidationService
{
    /// <summary>
    /// Проверка свободен ли никнейм
    /// </summary>
    /// <param name="username">Проверяемый никнейм</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Возвращает true - никнейм свободен/ false - никнейм занят</returns>
    public async Task<bool> IsAvailableUsernameAsync(string username, CancellationToken cancellationToken)
        => await userRepository.GetUserByUsernameAsync(username, cancellationToken) == null;

    /// <summary>
    /// Проверяет, свободен ли Email.
    /// </summary>
    /// <param name="email">Email для проверки.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>True, если Email свободен; иначе false.</returns>
    public async Task<bool> IsAvailableEmailAsync(string email, CancellationToken cancellationToken)
        => await userRepository.GetUserByEmailAsync(email, cancellationToken) == null;

    /// <summary>
    /// Валидирует пароль пользователя.
    /// </summary>
    /// <param name="validatePasswordModel">Модель данных для валидации пароля.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>True, если пароль корректен; иначе false.</returns>
    public async Task<bool> ValidatePasswordAsync(ValidatePasswordModel validatePasswordModel, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(validatePasswordModel.Id, cancellationToken);
        return user != null && hasher.VerifyHashedPassword(validatePasswordModel.Password, user.PasswordHash.Value);
    }

    /// <summary>
    /// Валидирует код подтверждения.
    /// </summary>
    /// <param name="requestId">Идентификатор запроса для получения кода.</param>
    /// <param name="requestCode">Код подтверждения для проверки.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>True, если код подтверждения верный; иначе false.</returns>
    public async Task<bool> ValidateVerificationCodeAsync(Guid requestId, ushort requestCode,
        CancellationToken cancellationToken)
        => await verificationCodeService.ValidateCodeByUserIdAsync(requestId, requestCode, cancellationToken);
}