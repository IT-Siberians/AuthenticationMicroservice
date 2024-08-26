using FluentValidation;
using Services.Abstractions;
using WebApiAuthenticate.Requests;
using WebApiAuthenticate.RequestsValidators.ObjectsValidators;

namespace WebApiAuthenticate.RequestsValidators;

public class CreatingUserValidator : AbstractValidator<CreatingUserRequest>
{
    public CreatingUserValidator(IUserChangeValidationService service)
    {
        RuleFor(request => request)
            .NotEmpty().WithMessage("Request should not be empty.");

        RuleFor(request => request.Username)
            .SetValidator(new UsernameValidator(service));

        RuleFor(request => request.Password)
            .SetValidator(new NewPasswordValidator());

        RuleFor(request => request.Email)
            .SetValidator(new EmailValidator(service));
    }
}