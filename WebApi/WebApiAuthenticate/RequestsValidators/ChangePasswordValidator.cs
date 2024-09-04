using FluentValidation;
using WebApiAuthenticate.Requests;
using WebApiAuthenticate.RequestsValidators.ObjectsValidators;
using static WebApiAuthenticate.RequestsValidators.Helpers.RequestHelpers.RequestValidationMessages;
using static WebApiAuthenticate.RequestsValidators.Helpers.PasswordHelpers.OldPasswordValidationMessages;

namespace WebApiAuthenticate.RequestsValidators;

public class ChangePasswordValidator : AbstractValidator<ChangePasswordRequest>
{

    public ChangePasswordValidator()
    {
        RuleFor(request => request)
            .NotEmpty().WithMessage(REQUEST_EMPTY_ERROR);

        RuleFor(request => request.OldPassword)
            .NotEmpty().WithMessage(OLD_PASSWORD_EMPTY_ERROR);

        RuleFor(request => request.NewPassword)
            .SetValidator(new NewPasswordValidator());
    }
}