using Services.Contracts;

namespace Services.Abstractions;

/// <summary>
/// Интерфейс создания токена подтверждения Email
/// </summary>
public interface IEmailTokenService
{

    /// <summary>
    /// Создание/шифрование токена подтверждения Email
    /// </summary>
    /// <param name="model">Модель генерации подтверждения</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Токен подтверждения Email</returns>
    Task<string> EncryptAsync(MailConfirmationGenerationModel model, CancellationToken cancellationToken);

    /// <summary>
    /// Расшифровка/преобразование зашифрованного токена в модель установки Email
    /// </summary>
    /// <param name="token">Зашифрованный токен</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Модель установки Email</returns>
    Task<SetUserEmailModel> DecryptAsync(string token, CancellationToken cancellationToken);
}