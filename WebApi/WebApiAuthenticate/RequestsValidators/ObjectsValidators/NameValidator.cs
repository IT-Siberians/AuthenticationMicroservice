using FluentValidation;
using static Common.Helpers.Constants.NameConstants;
using static WebApiAuthenticate.RequestsValidators.Helpers.NameHelpers.NameValidationMessages;

namespace WebApiAuthenticate.RequestsValidators.ObjectsValidators;

public class NameValidator : AbstractValidator<string>
{
    public NameValidator()
    {
        RuleFor(firstname => firstname)
            .NotEmpty().WithMessage(NAME_EMPTY_ERROR)
            .MaximumLength(NAME_MAX_LENGTH)
            .WithMessage(string.Format(NAME_MAX_LENGTH_ERROR,
                NAME_MAX_LENGTH))
            .Matches(NAME_CHARACHTER_SET_PATTERN)
            .WithMessage(string.Format(NAME_FORMAT_ERROR));
    }
}