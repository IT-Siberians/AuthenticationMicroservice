using Domain.Entities;
using Repositories.Abstractions;

namespace Repositories.Implementations.InMemoryRepository;

public class InMemoryUserRepository : BaseInMemoryRepository<User, Guid>, IUserRepository
{

    /// <summary>
    /// Проверить занят ли данное имя пользователя
    /// </summary>
    /// <param name="username">Имя пользователя</param>
    /// <returns>true - если имя пользователя свободно, false - имя пользователя занято</returns>
    public Task<bool> CheckIsAvailableUsernameAsync(string username)
    {
        var user = Entities.FirstOrDefault(u => u.Username.Value == username);
        return Task.FromResult(user == null);
    }

    /// <summary>
    /// Проверить занят ли данный Емейл
    /// </summary>
    /// <param name="email">Проверяемый Емейл</param>
    /// <returns>true - если Емейл свободен, false - Емейл занят</returns>
    public Task<bool> CheckIsAvailableEmailAsync(string email)
    {
        var user = Entities.FirstOrDefault(u => u.Email.Value == email);
        return Task.FromResult(user == null);
    }
}