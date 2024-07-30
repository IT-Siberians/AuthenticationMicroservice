using Domain.Entities;

namespace Repositories.Abstractions;

public interface IUserRepository : IBaseRepository<User, Guid>
{
    Task<bool> CheckIsAvailableUsernameAsync(string username);
    Task<bool> CheckIsAvailableEmailAsync(string email);
}