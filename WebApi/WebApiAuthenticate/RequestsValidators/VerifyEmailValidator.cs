using FluentValidation;
using Services.Abstractions;
using WebApiAuthenticate.Requests;
using WebApiAuthenticate.RequestsValidators.ObjectsValidators;

namespace WebApiAuthenticate.RequestsValidators;

public class VerifyEmailValidator : AbstractValidator<VerifyEmailRequest>
{
    public VerifyEmailValidator(IUserChangeValidationService service)
    {
        RuleFor(request => request)
            .NotEmpty().WithMessage("Request should not be empty.");

        RuleFor(request => request.Id)
            .SetValidator(new IdValidator());

        RuleFor(request => request.NewEmail)
            .SetValidator(new EmailValidator(service));

        RuleFor(request => request.CreatedDateTime)
            .MustAsync(IsLinkExpired).WithMessage("The link expired");
    }

    private Task<bool> IsLinkExpired(DateTime createdTime, CancellationToken cancellationToken)
    {
        var time = DateTime.Now - createdTime;
        return Task.FromResult(time.TotalMinutes > 15);
    }
}