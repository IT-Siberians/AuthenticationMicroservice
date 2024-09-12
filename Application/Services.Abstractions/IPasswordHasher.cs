namespace Services.Abstractions;

/// <summary>
/// Интерфейс шифровальщик пароля
/// </summary>
public interface IPasswordHasher
{
    /// <summary>
    /// Сгенерировать хэш пароля
    /// </summary>
    /// <param name="password">Хэшируемый пароль</param>
    /// <returns>Хэш пароля</returns>
    public string GenerateHashPassword(string password);

    /// <summary>
    /// Проверить пароль на совпадения хэша
    /// </summary>
    /// <param name="password">Проверяемый пароль</param>
    /// <param name="hashedPassword">Хэш пароль из источника</param>
    /// <returns>Возвращает true - проверяемый пароль совпадает с паролем из источника/ false - проверяемый пароль не совпадает с паролем из источника</returns>
    public bool VerifyHashedPassword(string password, string hashedPassword);
}