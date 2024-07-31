namespace Services.Contracts;
public class ChangeEmailDto
{
    public Guid Id { get; set; }
    public string? NewEmail { get; set; }
}