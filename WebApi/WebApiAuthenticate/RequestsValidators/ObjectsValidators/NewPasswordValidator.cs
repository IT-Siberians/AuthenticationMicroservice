using FluentValidation;

namespace WebApiAuthenticate.RequestsValidators.ObjectsValidators;

public class NewPasswordValidator : AbstractValidator<string>
{
    private const int MinPasswordLength = 8;
    private const string ExistenceCapitalLetterPattern = "[A-Z]";
    private const string ExistenceLowercaseLetterPattern = "[a-z]";
    private const string ExistenceDigitPattern = "[0-9]";

    public NewPasswordValidator()
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