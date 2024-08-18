using FluentValidation;
using WebApiAuthenticate.Models;
using WebApiAuthenticate.ModelsValidators.BaseValidators;

namespace WebApiAuthenticate.ModelsValidators;

public class VerifyEmailValidator : AbstractValidator<VerifyEmailRequest>
{
    public VerifyEmailValidator()
    {
        RuleFor(request => request)
            .NotEmpty().WithMessage("Request should not be empty.");

        RuleFor(request => request.Id)
            .SetValidator(new IdValidator());

        RuleFor(request => request.NewEmail)
            .SetValidator(new EmailValidator());
    }
}