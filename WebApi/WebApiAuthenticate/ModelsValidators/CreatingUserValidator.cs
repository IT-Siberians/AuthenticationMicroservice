using FluentValidation;
using Services.Abstractions;
using WebApiAuthenticate.Models;
using WebApiAuthenticate.ModelsValidators.BaseValidators;

namespace WebApiAuthenticate.ModelsValidators;

public class CreatingUserValidator : AbstractValidator<CreatingUserRequest>
{
    public CreatingUserValidator(IUserManagementService service)
    {
        RuleFor(request => request)
            .NotEmpty().WithMessage("Request should not be empty.");

        RuleFor(request => request.Username)
            .SetValidator(new UsernameValidator())
            .SetValidator(new AvailableUsernameValidator(service));

        RuleFor(request => request.Password)
            .SetValidator(new PasswordValidator());

        RuleFor(request => request.Email)
            .SetValidator(new EmailValidator())
            .SetValidator(new AvailableEmailValidator(service));
    }
}