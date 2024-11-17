using Microsoft.Extensions.Options;
using Redis;
using Repositories.Abstractions;
using Services.Contracts;

namespace Repositories.Implementations.RedisRepositories;

/// <summary>
/// Репозиторий для работы с кодами подтверждения, хранимыми в Redis.
/// Осуществляет операции получения, добавления и удаления кодов подтверждения для пользователей.
/// </summary>
public class VerificationCodeRepository : IVerificationCodeRepository
{
    private readonly VerificationCodeRepositoryOptions _options;

    /// <summary>
    /// Инициализирует новый экземпляр репозитория для работы с кодами подтверждения.
    /// </summary>
    /// <param name="options">Конфигурация для репозитория, включающая время хранения кода.</param>
    /// <param name="context">Контекст Redis, предоставляющий доступ к базе данных.</param>
    public VerificationCodeRepository(IOptions<VerificationCodeRepositoryOptions> options, RedisContext context)
    {
        _options = options.Value;
        Context = context;
    }

    /// <summary>
    /// Контекст Redis, используемый для взаимодействия с базой данных.
    /// </summary>
    private RedisContext Context { get; }

    /// <summary>
    /// Получает код подтверждения для пользователя по его уникальному идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
    /// <returns>Модель с кодом подтверждения или <c>null</c>, если код не найден.</returns>
    public async Task<VerificationCodeModel?> GetCodeByUserIdAsync(Guid id, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return null;

        var codeData = await Context.Database.StringGetAsync(id.ToString());

        return codeData.IsNullOrEmpty ? null : new VerificationCodeModel(id, ushort.Parse(codeData));
    }

    /// <summary>
    /// Добавляет новый код подтверждения для пользователя в Redis.
    /// </summary>
    /// <param name="model">Модель с кодом подтверждения, который нужно добавить.</param>
    /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
    /// <returns><c>true</c>, если код успешно добавлен, иначе <c>false</c>.</returns>
    public async Task<bool> AddCodeAsync(VerificationCodeModel model, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return false;

        return await Context.Database.StringSetAsync(
            model.Id.ToString(),
            model.VerificationCode.ToString(),
            expiry: TimeSpan.FromMinutes(_options.ExpiredTime));
    }

    /// <summary>
    /// Удаляет код подтверждения для пользователя по его уникальному идентификатору.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя, чей код подтверждения необходимо удалить.</param>
    /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
    /// <returns><c>true</c>, если код успешно удален, иначе <c>false</c>.</returns>
    public async Task<bool> DeleteCodeByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return false;

        return await Context.Database.KeyDeleteAsync(userId.ToString());
    }
}