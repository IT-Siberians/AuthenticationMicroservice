namespace WebApiAuthenticate.Models;

public class ChangeEmailRequest
{
    public Guid Id { get; set; }
    public string? NewEmail { get; set; }
}