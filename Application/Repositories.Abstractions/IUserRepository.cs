using Domain.Entities;

namespace Repositories.Abstractions;

public interface IUserRepository : IBaseRepository<User, Guid>
{

    /// <summary>
    /// Проверить занято ли данное Имя пользователя
    /// </summary>
    /// <param name="username">Проверяемое имя пользователя</param>
    /// <returns>true - если имя свободно, false - имя занято</returns>
    Task<bool> CheckIsAvailableUsernameAsync(string username);

    /// <summary>
    /// Проверить занят ли данный Емейл
    /// </summary>
    /// <param name="email">Проверяемый Емейл</param>
    /// <returns>true - если Емейл свободен, false - Емейл занят</returns>
    Task<bool> CheckIsAvailableEmailAsync(string email);
}