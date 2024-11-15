using Repositories.Abstractions;
using Services.Abstractions;
using Services.Contracts;

namespace Services.Implementations;

/// <summary>
/// Сервис валидации изменений пользователей
/// </summary>
/// <param name="userRepository">Репозиторий пользователей</param>
/// <param name="hasher">Шифровальщик пароля</param>
public class UserValidationService(
    IUserRepository userRepository,
    IPasswordHasher hasher,
    IVerificationCodeRepository verificationCodeRepository) : IUserValidationService
{
    /// <summary>
    /// Проверка свободно ли имя пользователя
    /// </summary>
    /// <param name="username">Проверяемое имя пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Возвращает true - имя пользователя свободно/ false - имя пользователя занято</returns>
    public async Task<bool> IsAvailableUsernameAsync(string username, CancellationToken cancellationToken)
        => await repository.GetUserByUsernameAsync(username, cancellationToken) == null;
    

    /// <summary>
    /// Проверка свободен ли Email
    /// </summary>
    /// <param name="email">Проверяемый Email</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Возвращает true - Email свободен/ false - Email занят</returns>
    public async Task<bool> IsAvailableEmailAsync(string email, CancellationToken cancellationToken)
        => await repository.GetUserByEmailAsync(email, cancellationToken) == null;
    

    /// <summary>
    /// Валидация пароля
    /// </summary>
    /// <param name="validatePasswordModel">Модель валидации пароля</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Возвращает true - пароль верный/ false - некорректный пароль</returns>
    public async Task<bool> ValidatePasswordAsync(ValidatePasswordModel validatePasswordModel, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(validatePasswordModel.Id, cancellationToken);
        return user != null && hasher.VerifyHashedPassword(validatePasswordModel.Password, user.PasswordHash.Value);
    }

    public async Task<bool> ValidateVerificationCodeAsync(Guid requestId, int requestCode, CancellationToken cancellationToken)
    {
        var code = await verificationCodeRepository.GetVerificationCodeByUserIdAsync(requestId, cancellationToken);
        if (code == null)
            return false;

        return requestCode == code.VerificationCode;
    }
}