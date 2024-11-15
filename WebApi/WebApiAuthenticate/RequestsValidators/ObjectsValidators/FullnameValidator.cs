using FluentValidation;
using static Common.Helpers.Constants.UserFullnameConstants;
using static WebApiAuthenticate.RequestsValidators.Helpers.FirstnameHelpers.FullnameValidationMessages;

namespace WebApiAuthenticate.RequestsValidators.ObjectsValidators;

public class FullnameValidator : AbstractValidator<string>
{
    public FullnameValidator()
    {
        RuleFor(firstname => firstname)
            .NotEmpty().WithMessage(FULLNAME_EMPTY_ERROR)
            .MaximumLength(USER_FULLNAME_MAX_LENGTH)
            .WithMessage(string.Format(FULLNAME_MAX_LENGTH_ERROR,
                USER_FULLNAME_MAX_LENGTH))
            .Matches(USER_FULLNAME_CHARACHTER_SET_PATTERN)
            .WithMessage(string.Format(FULLNAME_FORMAT_ERROR));
    }
}