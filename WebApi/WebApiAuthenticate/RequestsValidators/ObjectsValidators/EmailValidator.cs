using System.Text.RegularExpressions;
using Common.Helpers.EmailHelpers;
using Common.Helpers.UsernameHelpers;
using FluentValidation;
using static Common.Helpers.EmailHelpers.EmailValidationMessages;
using static Common.Helpers.EmailHelpers.EmailConstants;

namespace WebApiAuthenticate.RequestsValidators.ObjectsValidators;

public class EmailValidator : AbstractValidator<string>
{
    private static readonly Regex ValidateRegex = new(EMAIL_VALID_PATTERN,
        RegexOptions.IgnoreCase | RegexOptions.Compiled);
    public EmailValidator()
    {
        RuleFor(email => email)
            .NotEmpty().WithMessage(EMAIL_EMPTY_ERROR)
            .MaximumLength(UsernameConstants.USERNAME_MAX_LENGTH)
            .WithMessage(string.Format(EMAIL_LONGER_MAX_LENGTH_ERROR,EMAIL_MAX_LENGTH))
            .EmailAddress().WithMessage(EMAIL_FORMAT_ERROR)
            .Must(email=>ValidateRegex.Match(email).Success).WithMessage(EMAIL_FORMAT_ERROR);
    }
}