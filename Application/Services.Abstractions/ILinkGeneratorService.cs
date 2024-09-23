using Services.Contracts;

namespace Services.Abstractions;

/// <summary>
/// Интерфейс генерации ссылок для подтверждения Email
/// </summary>
public interface ILinkGeneratorService
{
    /// <summary>
    /// Сгенерировать ссылку для подтверждения Email
    /// </summary>
    /// <param name="model">Модель генерации подтверждения</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Ссылка подтверждения Email</returns>
    Task<string> GenerateLinkAsync(MailConfirmationGenerationModel model,
        CancellationToken cancellationToken);

    /// <summary>
    /// Получить модель установки Email из ссылки
    /// </summary>
    /// <param name="link">Ссылка подтверждения Email</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Модель установки Email</returns>
    Task<SetUserEmailModel> GetModelFromLink(string link, CancellationToken cancellationToken);
}