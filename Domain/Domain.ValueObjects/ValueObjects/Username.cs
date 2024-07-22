using Domain.ValueObjects.BaseEntities;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Domain.ValueObjects.ValueObjects;

public class Username(string value) : ValueObject<string>(value)
{
    public const int MinNameLength = 3;
    public const int MaxNameLength = 30;
    private const string ValidNamePattern = "(^[a-zA-Z_-]+$)";

    protected override void Validate(string value)
    {
        if ((value == null) ||
            (string.IsNullOrWhiteSpace(value)))
            throw new ArgumentNullException(nameof(value), "Username cannot null or empty");

        if (value.Length > MaxNameLength)
            throw new ArgumentOutOfRangeException(nameof(value),
                $"Invalid username  length. Maximum length is {MaxNameLength}. Username value: {value}");

        if (value.Count(char.IsLetterOrDigit) < MinNameLength)
            throw new ArgumentOutOfRangeException(nameof(value),
            $"Invalid username  length. the minimum number of alphanumeric characters is equal to {MinNameLength}. Username value: {value}");
        
        var isMatch = Regex.Match(value, ValidNamePattern, RegexOptions.IgnoreCase);
        if (!isMatch.Success)
                throw new ArgumentException($"The username contains invalid characters. Username value: {value}");
    }
}