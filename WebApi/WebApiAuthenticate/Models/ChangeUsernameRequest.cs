namespace WebApiAuthenticate.Models;

public class ChangeUsernameRequest
{
    public Guid Id { get; set; }
    public string Username { get; set; }
}