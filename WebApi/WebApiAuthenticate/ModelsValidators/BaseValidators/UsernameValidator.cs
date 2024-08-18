using FluentValidation;

namespace WebApiAuthenticate.ModelsValidators.BaseValidators;

public class UsernameValidator : AbstractValidator<string>
{
    private const int MinNameLength = 3;
    private const int MaxNameLength = 30;
    private const string ValidNamePattern = "(^[a-zA-Z_-]+$)";

    public UsernameValidator()
    {
        RuleFor(username => username)
            .NotEmpty().WithMessage("The email cannot be empty.")
            .Length(MinNameLength, MaxNameLength)
            .WithMessage(
                $"The username must be no more than {MaxNameLength} characters and no less than {MinNameLength}.")
            .Matches(ValidNamePattern).WithMessage("Invalid username format.");
    }
}