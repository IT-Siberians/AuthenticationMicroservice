namespace Services.Contracts;

public class VerifyEmailDto
{
    public Guid Id { get; set; }
    public string? NewEmail { get; set; }
}
