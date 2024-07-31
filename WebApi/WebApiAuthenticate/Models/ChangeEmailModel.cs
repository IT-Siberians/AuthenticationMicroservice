namespace WebApiAuthenticate.Models;

public class ChangeEmailModel
{
    public Guid Id { get; set; }
    public string? NewEmail { get; set; }
}