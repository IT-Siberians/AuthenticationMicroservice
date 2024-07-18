using Domain.ValueObjects.ValueObjects;

namespace Services.Contracts;
public class PublishVerifyEmailDto
{
    public Guid Id { get; set; }
    public Email NewEmail { get; set; } // вэлью обджект в ДТО ??? 
}
