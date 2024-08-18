using FluentValidation;
using Services.Abstractions;

namespace WebApiAuthenticate.ModelsValidators.BaseValidators;

public class AvailableEmailValidator : AbstractValidator<string>
{
    private readonly IUserManagementService _service;

    public AvailableEmailValidator(IUserManagementService service)
    {
        _service = service;

        RuleFor(email => email)
            .Must(CheckIsAvailableEmail).WithMessage("The email is already taken.");
    }
    private bool CheckIsAvailableEmail(string email)
    {
        return _service.CheckAvailableEmailAsync(email).Result;
    }
}