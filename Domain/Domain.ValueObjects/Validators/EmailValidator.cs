using Domain.ValueObjects.Exceptions;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects.Validators;

public class EmailValidator : Validator<string>
{
    public const int MaxEmailLength = 255;
    private const string ValidEmailPattern = @"[.\-_a-z0-9]+@([a-z0-9][\-a-z0-9]+\.)+[a-z]{2,6}";
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

        if (value.Length > MaxEmailLength)
        {
            ExceptionMessage = $"Invalid email address  length. Maximum length is {MaxEmailLength}. Email value: {value}";
            return false;
        }

        if (!IsValidEmailAddressFormat(value))
        {
            ExceptionMessage = "Invalid email address format. Email value: " + value;
            return false;
        }

        return true;
    }
    public override Exception GetValidationException() => new EmailValidationException(ExceptionMessage);
    private static bool IsValidEmailAddressFormat(string email)
    {
        var isMatch = Regex.Match(email, ValidEmailPattern, RegexOptions.IgnoreCase);
        return isMatch.Success;
    }

}