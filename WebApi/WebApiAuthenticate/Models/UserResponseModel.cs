using Domain.Entities.Enums;

namespace WebApiAuthenticate.Models
{
    public class UserResponseModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public AccountStatuses AccountStatus { get; set; }
    }
}
