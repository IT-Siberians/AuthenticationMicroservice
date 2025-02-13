using Domain.Entities;

namespace Repositories.Abstractions;

/// <summary>
/// Репозиторий пользователей
/// </summary>
public interface IUserRepository : IBaseRepository<User, Guid>
{
    /// <summary>
    /// Получить пользователя по Никнейм
    /// </summary>
    /// <param name="username">Никнейм искомого пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Пользователь с указанным Никнейм</returns>
    Task<User?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken);

    /// <summary>
    /// Получить пользователя по Email
    /// </summary>
    /// <param name="email">Email искомого пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Пользователь с указанным Email</returns>
    Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
}