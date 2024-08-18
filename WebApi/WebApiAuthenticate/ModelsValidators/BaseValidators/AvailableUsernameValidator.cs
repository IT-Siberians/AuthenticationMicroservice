using FluentValidation;
using Services.Abstractions;

namespace WebApiAuthenticate.ModelsValidators.BaseValidators;

public class AvailableUsernameValidator : AbstractValidator<string>
{
    private readonly IUserManagementService _service;

    public AvailableUsernameValidator(IUserManagementService service)
    {
        _service = service;

        RuleFor(username=>username)
            .Must(CheckIsAvailableUsername).WithMessage("The username is already taken.");
    }
    private bool CheckIsAvailableUsername(string username)
    {
        return _service.CheckAvailableUsernameAsync(username).Result;
    }
}