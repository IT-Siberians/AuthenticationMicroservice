namespace Services.Contracts;
public class ChangePasswordDto
{
    public Guid Id { get; set; }
    public string? Password { get; set; }
    public string? NewPassword { get; set; }
}