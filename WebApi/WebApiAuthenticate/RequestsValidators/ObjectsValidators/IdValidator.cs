using FluentValidation;
using static Common.Helpers.IdHelpers.IdValidationMessages;

namespace WebApiAuthenticate.RequestsValidators.ObjectsValidators;

public class IdValidator : AbstractValidator<Guid>
{
    public IdValidator()
    {
        RuleFor(id => id)
            .NotEmpty().WithMessage(ID_EMPTY_ERROR);
    }
}
