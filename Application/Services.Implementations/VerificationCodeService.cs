using Repositories.Abstractions;
using Services.Abstractions;
using Services.Contracts;

namespace Services.Implementations;

/// <summary>
/// Сервис для работы с кодами верификации.
/// </summary>
/// <param name="repository">Репозиторий для работы с кодами верификации.</param>
public class VerificationCodeService(
    IVerificationCodeRepository repository) : IVerificationCodeService
{
    /// <summary>
    /// Генерирует новый код верификации для указанного пользователя и сохраняет его в репозитории.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя, для которого генерируется код.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Сгенерированный код верификации.</returns>
    public async Task<ushort> GenerateCodeAsync(Guid userId, CancellationToken cancellationToken)
    {
        var code = (ushort)new Random().Next(10000, ushort.MaxValue);

        await repository.AddVerificationCodeAsync(new VerificationCodeModel(userId, code), cancellationToken);

        return code;
    }

    /// <summary>
    /// Получает код верификации для указанного пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель с кодом верификации или null, если код не найден.</returns>
    public async Task<VerificationCodeModel?> GetCodeByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        => await repository.GetVerificationCodeByUserIdAsync(userId, cancellationToken);
}