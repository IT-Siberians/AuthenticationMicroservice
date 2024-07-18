using Domain.ValueObjects.Exceptions;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects.Validators;
public class UsernameValidator : Validator<string>
{
    public const int MinNameLength = 3;
    public const int MaxNameLength = 30;
    private const string ValidNamePattern = "(^[a-zA-Z_-]+$)";

    public override bool Validate(string? value)
    {
        if (value == null)
        {
            ExceptionMessage = "Email cannot null";
            return false;
        }

        if (string.IsNullOrEmpty(value))
        {
            ExceptionMessage = "Email cannot empty";
            return false;
        }

        if (value.Length > MaxNameLength)
        {
            ExceptionMessage = $"Invalid username  length. Maximum length is {MaxNameLength}. Username value: {value}";
            return false;
        }
        if (CountAlphanumericCharacters(value) < MinNameLength)
        {
            ExceptionMessage = $"Invalid username  length. the minimum number of alphanumeric characters is equal to {MinNameLength}. Username value: {value}";
            return false;
        }
        if (!IsValidCharacterSet(value))
        {
            ExceptionMessage = "The username contains invalid characters. Username value: " + value;
            return false;
        }

        return true;

    }
    public override Exception GetValidationException() => new UsernameValidationException(ExceptionMessage);

    private static bool IsValidCharacterSet(string name)
    {
        var isMatch = Regex.Match(name, ValidNamePattern, RegexOptions.IgnoreCase);
        return isMatch.Success;
    }

    private static int CountAlphanumericCharacters(string name)
        => name.Count(char.IsLetterOrDigit);

}