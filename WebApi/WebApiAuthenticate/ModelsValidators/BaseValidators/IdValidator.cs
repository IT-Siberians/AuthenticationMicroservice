using FluentValidation;

namespace WebApiAuthenticate.ModelsValidators.BaseValidators;

public class IdValidator : AbstractValidator<Guid>
{
    public IdValidator()
    {
        RuleFor(id => id)
            .NotEmpty().WithMessage("The id cannot be empty.");
    }
}
