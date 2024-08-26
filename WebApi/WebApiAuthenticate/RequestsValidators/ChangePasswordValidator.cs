using FluentValidation;
using Services.Abstractions;
using Services.Contracts;
using WebApiAuthenticate.Requests;
using WebApiAuthenticate.RequestsValidators.ObjectsValidators;

namespace WebApiAuthenticate.RequestsValidators;

public class ChangePasswordValidator : AbstractValidator<ChangePasswordRequest>
{
    private readonly IUserChangeValidationService _service;

    public ChangePasswordValidator(IUserChangeValidationService service)
    {
        _service = service;
        RuleFor(request => request)
            .NotEmpty().WithMessage("Request should not be empty.");

        RuleFor(request => request.Id)
            .SetValidator(new IdValidator());

        RuleFor(request => request.OldPassword)
            .MustAsync(ValidatePassword).WithMessage($"Invalid password");

        RuleFor(request => request.NewPassword)
            .SetValidator(new NewPasswordValidator());
    }

    private async Task<bool> ValidatePassword(ChangePasswordRequest request, string oldPassword, CancellationToken cancellationToken)
    {
        var model = new ValidatePasswordModel() { Id = request.Id, Password = oldPassword };
        return  await _service.ValidatePasswordAsync(model, cancellationToken); // это должно выполняться синхронно так как FluentValidation не поддерживает асинхронные вызовы
    }
}