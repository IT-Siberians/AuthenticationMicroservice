using Domain.Entities;
using Repositories.Abstractions;

namespace Repositories.Implementations.InMemoryRepository;
public class InMemoryUserRepository : BaseInMemoryRepository<User, Guid>, IUserRepository
{
    public Task<bool> CheckIsAvailableUsernameAsync(string username)
    {
        var user = Entities.FirstOrDefault(u => u.Username.Value == username);
        return Task.FromResult(user == null);
    }

    public Task<bool> CheckIsAvailableEmailAsync(string email)
    {
        var user = Entities.FirstOrDefault(u => u.Email.Value == email);
        return Task.FromResult(user == null);
    }
}