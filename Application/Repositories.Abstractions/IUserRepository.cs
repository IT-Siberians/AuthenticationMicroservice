using Domain.Entities;

namespace Repositories.Abstractions;

/// <summary>
/// Репозиторий пользователей
/// </summary>
public interface IUserRepository : IBaseRepository<User, Guid>
{
    /// <summary>
    /// Получить пользователя по имени пользователя(никнейму)
    /// </summary>
    /// <param name="username">Имя пользователя(никнейм) искомого пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Пользователь с указанным именем пользователя(никнеймом)</returns>
    Task<User?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken);

    /// <summary>
    /// Получить пользователя по Email
    /// </summary>
    /// <param name="email">Email искомого пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Пользователь с указанным Email</returns>
    Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
}