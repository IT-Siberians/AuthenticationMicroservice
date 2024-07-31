namespace WebApiAuthenticate.Models;

public class VerifyEmailModel
{
    public Guid Id { get; set; }
    public string? NewEmail { get; set; }
    public DateTime CreatedDateTime { get; set; }
}