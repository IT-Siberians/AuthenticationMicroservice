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

        await repository.AddCodeAsync(new VerificationCodeModel(userId, code), cancellationToken);

        return code;
    }

    /// <summary>
    /// Получает код верификации для указанного пользователя.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="code">Проверяемый код подтверждения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель с кодом верификации или null, если код не найден.</returns>
    public async Task<bool> ValidateCodeByUserIdAsync(
        Guid id,
        ushort code,
        CancellationToken cancellationToken)
    {
        if (code == 0)
        {
            await repository.DeleteCodeByIdAsync(id, cancellationToken);
            return true;
        }

        var model = await repository.GetCodeByUserIdAsync(id, cancellationToken);
        if (model is null)
            return false;

        if (model.VerificationCode != code) return false;
        await repository.DeleteCodeByIdAsync(id, cancellationToken);
        return true;
    }
}