namespace Services.Contracts;

public class VerifyEmailDto
{
    public Guid UserId { get; set; }
    public string VerifiedEmail { get; set; }
}