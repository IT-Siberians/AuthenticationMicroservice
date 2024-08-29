using Domain.Entities;
using Domain.ValueObjects.ValueObjects;
using Repositories.Abstractions;

namespace Repositories.Implementations.InMemoryRepository;

/// <summary>
/// Репозиторий с CRUD операциями
/// </summary>
/// <typeparam name="T">Сущность репозитория</typeparam>
/// <typeparam name="TId">Идентификатор репозитория</typeparam>
public class InMemoryUserRepository : BaseInMemoryRepository<User, Guid>,
    IUserRepository
{
    /// <summary>
    /// Получить пользователя по имени пользователя(никнейму)
    /// </summary>
    /// <param name="username">Имя пользователя(никнейм) искомого пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Пользователь с указанным именем пользователя(никнеймом)</returns>
    public async Task<User?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return await Task.Run(()=>Entities.FirstOrDefault(u => u.Username.Value == username), cancellationToken);
    }

    /// <summary>
    /// Получить пользователя по Email
    /// </summary>
    /// <param name="email">Email искомого пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Пользователь с указанным Email</returns>
    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return await Task.Run(() => Entities.FirstOrDefault(u => u.Email.Value == email), cancellationToken);
    }
}