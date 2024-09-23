using Microsoft.Extensions.Options;
using Services.Abstractions;
using Services.Contracts;
using Services.Implementations.Options;

namespace Services.Implementations;

/// <summary>
/// Сервис генерации ссылок для подтверждения Email
/// </summary>
public class LinkGeneratorService(
    IOptions<LinkGeneratorOptions> options,
    IEmailTokenService emailTokenService) : ILinkGeneratorService
{
    private readonly LinkGeneratorOptions _options = options.Value;

    /// <summary>
    /// Сгенерировать ссылку для подтверждения Email
    /// </summary>
    /// <param name="model">Модель генерации подтверждения</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Ссылка подтверждения Email</returns>
    public async Task<string> GenerateLinkAsync(MailConfirmationGenerationModel model,
        CancellationToken cancellationToken)
    {
        var encryptedData = await emailTokenService.EncryptAsync(model, cancellationToken);
        var encodedData = Uri.EscapeDataString(encryptedData);
        var result = $"{_options.DomainPath}/{_options.ControllerPath}/{_options.ActionPath}?link={encodedData}";
        Console.WriteLine(result);
        return result;
    }

    /// <summary>
    /// Получить модель установки Email из ссылки
    /// </summary>
    /// <param name="link">Ссылка подтверждения Email</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Модель установки Email</returns>
    public async Task<SetUserEmailModel> GetModelFromLink(string link, CancellationToken cancellationToken)
    {
        var encryptedData = Uri.UnescapeDataString(link);
        return await emailTokenService.DecryptAsync(encryptedData, cancellationToken);
    }
}