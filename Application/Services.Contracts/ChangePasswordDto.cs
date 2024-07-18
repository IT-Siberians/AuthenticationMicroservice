namespace Services.Contracts;

public class ChangePasswordDto
{
    public Guid UserId { get; set; }
    public string PasswordToChange { get; set; }
    public string PasswordVerification { get; set; }
}