using FluentValidation;
using WebApiAuthenticate.Requests;
using WebApiAuthenticate.RequestsValidators.ObjectsValidators;
using static Common.Helpers.RequestHelpers.RequestValidationMessages;

namespace WebApiAuthenticate.RequestsValidators;

public class ChangeUsernameValidator : AbstractValidator<ChangeUsernameRequest>
{
    public ChangeUsernameValidator()
    {
        RuleFor(request => request)
            .NotEmpty().WithMessage(REQUEST_EMPTY_ERROR);

        RuleFor(request => request.Id)
            .SetValidator(new IdValidator());

        RuleFor(request => request.NewUsername)
            .SetValidator(new UsernameValidator());
    }
}