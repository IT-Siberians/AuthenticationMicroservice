using Repositories.Abstractions;
using Services.Abstractions;
using Services.Contracts;

namespace Services.Implementations;

/// <summary>
/// Сервис валидации изменений пользователей.
/// </summary>
/// <param name="userRepository">Репозиторий пользователей.</param>
/// <param name="hasher">Интерфейс для работы с хешированием паролей.</param>
/// <param name="verificationCodeService">Сервис работы с кодами подтверждения.</param>
public class UserValidationService(
    IUserRepository userRepository,
    IPasswordHasher hasher,
    IVerificationCodeService verificationCodeService) : IUserValidationService
{
    /// <summary>
    /// Проверяет, свободно ли имя пользователя.
    /// </summary>
    /// <param name="username">Имя пользователя для проверки.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>True, если имя пользователя свободно; иначе false.</returns>
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
    public async Task<bool> ValidateVerificationCodeAsync(Guid requestId, int requestCode, CancellationToken cancellationToken)
    {
        var code = await verificationCodeService.GetCodeByUserIdAsync(requestId, cancellationToken);
        if (code == null)
            return false;

        return requestCode == code.VerificationCode;
    }
}