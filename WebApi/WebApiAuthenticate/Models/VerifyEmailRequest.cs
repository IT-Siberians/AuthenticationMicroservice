namespace WebApiAuthenticate.Models;

public class VerifyEmailRequest
{
    public Guid Id { get; set; }
    public string NewEmail { get; set; }
    public DateTime CreatedDateTime { get; set; }
}