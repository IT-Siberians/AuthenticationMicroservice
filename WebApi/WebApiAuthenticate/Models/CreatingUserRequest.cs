namespace WebApiAuthenticate.Models;

public class CreatingUserRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}