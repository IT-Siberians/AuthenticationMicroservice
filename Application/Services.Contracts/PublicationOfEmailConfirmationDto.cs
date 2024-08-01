namespace Services.Contracts;
public class PublicationOfEmailConfirmationDto
{
    public Guid Id { get; set; }
    public string? NewEmail { get; set; }
}