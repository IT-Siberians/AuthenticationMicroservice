using Domain.Entities;
using Repositories.Abstractions;

namespace Repositories.Implementations.InMemoryRepository;
public class InMemoryUserRepository : BaseInMemoryRepository<User, Guid>, IUserRepository
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
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