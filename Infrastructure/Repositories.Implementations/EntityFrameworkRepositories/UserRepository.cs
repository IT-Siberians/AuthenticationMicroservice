using Domain.Entities;
using EntityFramework;
using Microsoft.EntityFrameworkCore;
using Repositories.Abstractions;

namespace Repositories.Implementations.EntityFrameworkRepositories;

/// <summary>
/// Репозиторий пользователей с использованием EF
/// </summary>
public class UserRepository(UserDbContext databaseContext) : BaseEntityFrameworkRepository<User, Guid>(databaseContext),
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
        return await EntitySet.Where(u => u.IsDeleted == false)
            .FirstOrDefaultAsync(u => u.Username.Value == username, cancellationToken);
    }

    /// <summary>
    /// Получить пользователя по Email
    /// </summary>
    /// <param name="email">Email искомого пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Пользователь с указанным Email</returns>
    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await EntitySet.Where(u => u.IsDeleted == false)
            .FirstOrDefaultAsync(u => u.Username.Value == email, cancellationToken);
    }
}