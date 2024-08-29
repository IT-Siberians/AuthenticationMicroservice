using FluentValidation;
using static Common.Helpers.PasswordHelpers.NewPasswordConstants;
using static Common.Helpers.PasswordHelpers.NewPasswordValidationMessages;

namespace WebApiAuthenticate.RequestsValidators.ObjectsValidators;

public class NewPasswordValidator : AbstractValidator<string>
{
    public NewPasswordValidator()
    {
        RuleFor(password => password)
            .NotEmpty().WithMessage(NEW_PASSWORD_EMPTY_ERROR)
            .MinimumLength(NEW_PASSWORD_MIN_LENGTH).WithMessage(NEW_PASSWORD_MIN_LENGTH_ERROR)
            .Matches(NEW_PASSWORD_EXIST_CAPITAL_LETTER_PATTERN).WithMessage(NEW_PASSWORD_NOT_EXIST_CAPITAL_LETTER_ERROR)
            .Matches(NEW_PASSWORD_EXIST_LOWERCASE_LETTER_PATTERN).WithMessage(NEW_PASSWORD_NOT_EXIST_LOWERCASE_LETTER_ERROR)
            .Matches(NEW_PASSWORD_EXIST_DIGIT_PATTERN).WithMessage(NEW_PASSWORD_NOT_EXIST_DIGIT_ERROR)
            .Matches(NEW_PASSWORD_EXIST_SPECIAL_SYMBOL_PATTERN).WithMessage(NEW_PASSWORD_NOT_EXIST_SPECIAL_SYMBOL_ERROR);
    }
}