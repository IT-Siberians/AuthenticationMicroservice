using Domain.Entities;
using Domain.ValueObjects.ValueObjects;
using EntityFramework;
using Microsoft.EntityFrameworkCore;
using Repositories.Abstractions;

namespace Repositories.Implementations.EntityFrameworkRepositories;

public class UserRepository(UserDbContext context) : BaseEntityFrameworkRepository<User, Guid>(context), IUserRepository
{
    public async Task<bool> CheckIsAvailableUsernameAsync(string username)
    {
        return EntitySet.ToList().FirstOrDefault(u => u.Username.Value == username) == null;
    }

    public async Task<bool> CheckIsAvailableEmailAsync(string email)
    {
        return EntitySet.ToList().FirstOrDefault(u => u.Email.Value == email) == null;
    }
}