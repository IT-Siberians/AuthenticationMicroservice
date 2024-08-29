using FluentValidation;
using WebApiAuthenticate.Requests;
using WebApiAuthenticate.RequestsValidators.ObjectsValidators;
using static Common.Helpers.RequestHelpers.RequestValidationMessages;
using static Common.Helpers.PasswordHelpers.OldPasswordValidationMessages;

namespace WebApiAuthenticate.RequestsValidators;

public class ChangePasswordValidator : AbstractValidator<ChangePasswordRequest>
{

    public ChangePasswordValidator()
    {
        RuleFor(request => request)
            .NotEmpty().WithMessage(REQUEST_EMPTY_ERROR);

        RuleFor(request => request.Id)
            .SetValidator(new IdValidator());

        RuleFor(request => request.OldPassword)
            .NotEmpty().WithMessage(OLD_PASSWORD_EMPTY_ERROR);

        RuleFor(request => request.NewPassword)
            .SetValidator(new NewPasswordValidator());
    }
}