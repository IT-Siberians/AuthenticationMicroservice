using System.Text.RegularExpressions;
using FluentValidation;

namespace WebApiAuthenticate.ModelsValidators.BaseValidators;

public class EmailValidator : AbstractValidator<string>
{
    private const int MaxEmailLength = 255;
    private const string ValidEmailPattern = @"[.\-_a-z0-9]+@([a-z0-9][\-a-z0-9]+\.)+[a-z]{2,6}";
    public EmailValidator()
    {
        RuleFor(email => email)
            .NotEmpty().WithMessage("The email cannot be empty.")
            .MaximumLength(MaxEmailLength)
            .WithMessage($"The email should be no more than {MaxEmailLength}.")
            .EmailAddress().WithMessage("Invalid email address format.")
            .Matches(ValidEmailPattern, RegexOptions.IgnoreCase).WithMessage("Invalid email address format.");
    }
}