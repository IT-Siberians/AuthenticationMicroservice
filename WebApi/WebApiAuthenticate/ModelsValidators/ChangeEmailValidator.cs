using FluentValidation;
using Services.Abstractions;
using WebApiAuthenticate.Models;
using WebApiAuthenticate.ModelsValidators.BaseValidators;

namespace WebApiAuthenticate.ModelsValidators;

public class ChangeEmailValidator : AbstractValidator<ChangeEmailRequest>
{
    public ChangeEmailValidator(IUserManagementService service)
    {
        RuleFor(request=> request)
            .NotEmpty().WithMessage("Request should not be empty.");

        RuleFor(request => request.Id)
            .SetValidator(new IdValidator());

        RuleFor(request => request.NewEmail)
            .SetValidator(new EmailValidator())
            .SetValidator(new AvailableEmailValidator(service));
    }
}
