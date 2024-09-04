using FluentValidation;
using static WebApiAuthenticate.RequestsValidators.Helpers.IdHelpers.IdValidationMessages;

namespace WebApiAuthenticate.RequestsValidators.ObjectsValidators;

public class IdValidator : AbstractValidator<Guid>
{
    public IdValidator()
    {
        RuleFor(id => id)
            .NotEmpty().WithMessage(ID_EMPTY_ERROR);
    }
}
