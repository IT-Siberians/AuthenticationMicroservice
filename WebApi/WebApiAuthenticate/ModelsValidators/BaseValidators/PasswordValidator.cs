using FluentValidation;

namespace WebApiAuthenticate.ModelsValidators.BaseValidators;

public class PasswordValidator : AbstractValidator<string>
{
    private const int MinPasswordLength = 8;
    private const string ExistenceCapitalLetterPattern = "[A - Z]";
    private const string ExistenceLowercaseLetterPattern = "[A - Z]";
    private const string ExistenceDigitPattern = "[A - Z]";

    public PasswordValidator()
    {
        RuleFor(password => password)
            .NotEmpty().WithMessage("The password cannot be empty.")
            .MinimumLength(MinPasswordLength).WithMessage("The password must contain at least 8 characters.")
            .Matches(ExistenceCapitalLetterPattern).WithMessage("The password must contain at least one capital letter.")
            .Matches(ExistenceLowercaseLetterPattern).WithMessage("The password must contain at least one lowercase letter.")
            .Matches(ExistenceDigitPattern).WithMessage("The password must contain at least one digit.")
            .Matches(@"[!@#$%^&*(),.?""{}|<>]").WithMessage("The password must contain at least one special character.");
    }
}