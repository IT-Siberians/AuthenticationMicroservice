using FluentValidation;

namespace WebApiAuthenticate.RequestsValidators.ObjectsValidators;

public class IdValidator : AbstractValidator<Guid>
{
    public IdValidator()
    {
        RuleFor(id => id)
            .NotEmpty().WithMessage("The id cannot be empty.");
    }
}
