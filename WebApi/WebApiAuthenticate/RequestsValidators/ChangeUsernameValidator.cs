using FluentValidation;
using Services.Abstractions;
using WebApiAuthenticate.Requests;
using WebApiAuthenticate.RequestsValidators.ObjectsValidators;

namespace WebApiAuthenticate.RequestsValidators;

public class ChangeUsernameValidator : AbstractValidator<ChangeUsernameRequest>
{
    public ChangeUsernameValidator(IUserChangeValidationService service)
    {
        RuleFor(request => request)
            .NotEmpty().WithMessage("Request should not be empty.");

        RuleFor(request => request.Id)
            .SetValidator(new IdValidator());

        RuleFor(request => request.NewUsername)
            .SetValidator(new UsernameValidator(service));
    }
}