using Domain.Entities;

namespace Repositories.Abstractions;
public interface IUserRepository
{
    Task<User> GetByIdAsync(Guid id);
    Task<Guid> AddAsync(User user);
    Task<bool> UpdateUserAsync(Guid id, User newUser);
}
