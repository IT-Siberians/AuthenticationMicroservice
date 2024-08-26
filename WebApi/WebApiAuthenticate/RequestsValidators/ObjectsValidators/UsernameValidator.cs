using FluentValidation;
using Services.Abstractions;

namespace WebApiAuthenticate.RequestsValidators.ObjectsValidators;

public class UsernameValidator : AbstractValidator<string>
{
    private readonly IUserChangeValidationService _service;
    private const int MinNameLength = 3;
    private const int MaxNameLength = 30;
    private const string ValidNamePattern = "(^[a-zA-Z_-]+$)";

    public UsernameValidator(IUserChangeValidationService service)
    {
        _service = service;
        RuleFor(username => username)
            .NotEmpty().WithMessage("The email cannot be empty.")
            .Length(MinNameLength, MaxNameLength)
            .WithMessage(
                $"The username must be no more than {MaxNameLength} characters and no less than {MinNameLength}.")
            .Matches(ValidNamePattern).WithMessage("Invalid username format.")
            .MustAsync(CheckIsAvailableUsername).WithMessage("The username is already taken.");
    }
    private async Task<bool> CheckIsAvailableUsername(string username, CancellationToken cancellationToken)
    {
        return await _service.IsAvailableUsernameAsync(username, cancellationToken);// это должно выполняться синхронно так как FluentValidation не поддерживает синхронные вызовы
    }
}
