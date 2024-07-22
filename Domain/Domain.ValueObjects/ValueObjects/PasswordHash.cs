using Domain.ValueObjects.BaseEntities;

namespace Domain.ValueObjects.ValueObjects;
public class PasswordHash(string value) : ValueObject<string> (value)
{
    protected override void Validate(string value)
    {
        if ((value == null) ||
            (string.IsNullOrWhiteSpace(value)))
            throw new ArgumentNullException(nameof(value), "PasswordHash cannot null or empty");
    }
}