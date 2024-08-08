namespace Services.Contracts;

public class VerifyEmailModel
{
    public Guid Id { get; set; }
    public string? NewEmail { get; set; }
}
