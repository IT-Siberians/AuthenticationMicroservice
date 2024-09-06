using BCrypt.Net;
using Services.Abstractions;

namespace PasswordHasher;

/// <summary>
/// Шифровальщик пароля
/// </summary>
public class CustomPasswordHasher : IPasswordHasher
{
    /// <summary>
    /// Тип шифрования
    /// </summary>
    private const HashType HashType = BCrypt.Net.HashType.SHA512;

    /// <summary>
    /// Добавить дополнительную случайность к процессу хеширования: true - да, false - нет
    /// </summary>
    private const bool EnhancedEntropy = true;

    /// <summary>
    /// Количество итераций, используемых при хешировании пароля
    /// </summary>
    private const int WorkFactor = 12;

    /// <summary>
    /// Сгенерировать хэш пароля
    /// </summary>
    /// <param name="password">Хэшируемый пароль</param>
    /// <returns>Хэш пароля</returns>

    public string GenerateHashPassword(string password)
    {
        var salt = BCrypt.Net.BCrypt.GenerateSalt(WorkFactor);

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt, EnhancedEntropy, HashType);

        return hashedPassword;
    }

    /// <summary>
    /// Проверить пароль на совпадения хэша
    /// </summary>
    /// <param name="password">Проверяемый пароль</param>
    /// <param name="hashedPassword">Хэш пароль из источника</param>
    /// <returns>Возвращает true - проверяемый пароль совпадает с паролем из источника/ false - проверяемый пароль не совпадает с паролем из источника</returns>
    public bool VerifyHashedPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword, EnhancedEntropy, HashType);
    }
}