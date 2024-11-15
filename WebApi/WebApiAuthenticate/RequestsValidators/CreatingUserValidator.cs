using FluentValidation;
using WebApiAuthenticate.Requests;
using WebApiAuthenticate.RequestsValidators.ObjectsValidators;
using static WebApiAuthenticate.RequestsValidators.Helpers.RequestHelpers.RequestValidationMessages;

namespace WebApiAuthenticate.RequestsValidators;

public class CreatingUserValidator : AbstractValidator<CreatingUserRequest>
{
    public CreatingUserValidator()
    {
        RuleFor(request => request)
            .NotEmpty().WithMessage(REQUEST_EMPTY_ERROR);

        RuleFor(request => request.Username)
            .SetValidator(new UsernameValidator());

        RuleFor(request => request.FirstName)
            .SetValidator(new FullnameValidator());

        RuleFor(request => request.Lastname)
            .SetValidator(new FullnameValidator());

        RuleFor(request => request.Password)
            .SetValidator(new NewPasswordValidator());

        RuleFor(request => request.Email)
            .SetValidator(new EmailValidator());
    }
}