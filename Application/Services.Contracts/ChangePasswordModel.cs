namespace Services.Contracts;
public class ChangePasswordModel
{
    public Guid Id { get; set; }
    public string? Password { get; set; }
    public string? NewPassword { get; set; }
}