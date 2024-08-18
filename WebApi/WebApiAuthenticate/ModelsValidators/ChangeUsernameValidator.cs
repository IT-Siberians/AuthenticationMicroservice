using FluentValidation;
using Services.Abstractions;
using WebApiAuthenticate.Models;
using WebApiAuthenticate.ModelsValidators.BaseValidators;

namespace WebApiAuthenticate.ModelsValidators;

public class ChangeUsernameValidator : AbstractValidator<ChangeUsernameRequest>
{
    public ChangeUsernameValidator(IUserManagementService service)
    {
        RuleFor(request => request)
            .NotEmpty().WithMessage("Request should not be empty.");

        RuleFor(request => request.Id)
            .SetValidator(new IdValidator());

        RuleFor(request => request.Username)
            .SetValidator(new UsernameValidator())
            .SetValidator(new AvailableUsernameValidator(service));
    }
}