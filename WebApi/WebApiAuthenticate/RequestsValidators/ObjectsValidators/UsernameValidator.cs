using FluentValidation;
using static Common.Helpers.Constants.UsernameConstants;
using static WebApiAuthenticate.RequestsValidators.Helpers.UsernameHelpers.UsernameValidationMessages;

namespace WebApiAuthenticate.RequestsValidators.ObjectsValidators;

public class UsernameValidator : AbstractValidator<string>
{
    public UsernameValidator()
    {
        RuleFor(username => username)
            .NotEmpty().WithMessage(USERNAME_EMPTY_ERROR)
            .Length(USERNAME_MIN_LENGTH, USERNAME_MAX_LENGTH)
            .WithMessage(string.Format(USERNAME_LENGTH_ERROR, USERNAME_MAX_LENGTH, USERNAME_MIN_LENGTH))
            .Matches(USERNAME_VALID_PATTERN).WithMessage(USERNAME_FORMAT_ERROR);
    }
}
