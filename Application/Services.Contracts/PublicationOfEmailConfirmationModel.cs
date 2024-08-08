namespace Services.Contracts;
public class PublicationOfEmailConfirmationModel
{
    public Guid Id { get; set; }
    public string? NewEmail { get; set; }
}