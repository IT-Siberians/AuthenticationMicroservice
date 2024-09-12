using FluentValidation;
using WebApiAuthenticate.Requests;
using WebApiAuthenticate.RequestsValidators.ObjectsValidators;
using static WebApiAuthenticate.RequestsValidators.Helpers.RequestHelpers.RequestValidationMessages;

namespace WebApiAuthenticate.RequestsValidators;

public class VerifyEmailValidator : AbstractValidator<ConfirmEmailRequest>
{
    public VerifyEmailValidator()
    {
        RuleFor(request => request)
            .NotEmpty().WithMessage(REQUEST_EMPTY_ERROR);

        RuleFor(request => request.Id)
            .SetValidator(new IdValidator());

        RuleFor(request => request.NewEmail)
            .SetValidator(new EmailValidator());
    }
}