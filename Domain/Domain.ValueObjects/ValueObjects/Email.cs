using Domain.ValueObjects.BaseEntities;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects.ValueObjects;
public class Email(string value) : ValueObject<string>(value)
{
    public const int MaxEmailLength = 255;
    private const string ValidEmailPattern = @"[.\-_a-z0-9]+@([a-z0-9][\-a-z0-9]+\.)+[a-z]{2,6}";
    protected override void Validate(string? value)
    {
        if ((value == null)||
            (string.IsNullOrWhiteSpace(value)))
            throw new ArgumentNullException(nameof(value), "Email cannot null or empty");

        if (value.Length > MaxEmailLength)
            throw new ArgumentOutOfRangeException(
                $"Invalid email address  length. Maximum length is {MaxEmailLength}. Email value: {value}");
        
        var isMatch = Regex.Match(value, ValidEmailPattern, RegexOptions.IgnoreCase);
        if (!isMatch.Success)
            throw new ArgumentException(
            $"Invalid email address format. Email value: {value}");
    }
}