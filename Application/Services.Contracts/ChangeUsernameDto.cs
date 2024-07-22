namespace Services.Contracts;
public class ChangeUsernameDto
{
    public string NewUsername { get; set; }
    public Guid UserId { get; set; }
}