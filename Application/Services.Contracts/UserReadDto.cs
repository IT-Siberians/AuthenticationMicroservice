using Domain.Entities.Enums;

namespace Services.Contracts;
public class UserReadDto
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }
    public AccountStatuses AccountStatus { get; set; }

}