using FluentValidation;
using WebApiAuthenticate.Models;
using WebApiAuthenticate.ModelsValidators.BaseValidators;

namespace WebApiAuthenticate.ModelsValidators;

public class ChangePasswordValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordValidator()
    {
        RuleFor(request => request)
            .NotEmpty().WithMessage("Request should not be empty.");

        RuleFor(request => request.Id)
            .SetValidator(new IdValidator());

        //не делаю валидацию обычного пароля

        RuleFor(request => request.NewPassword)
            .SetValidator(new PasswordValidator());
    }
}