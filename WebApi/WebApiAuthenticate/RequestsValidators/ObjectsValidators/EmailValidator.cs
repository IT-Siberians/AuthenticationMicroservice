using System.Text.RegularExpressions;
using FluentValidation;
using Services.Abstractions;

namespace WebApiAuthenticate.RequestsValidators.ObjectsValidators;

public class EmailValidator : AbstractValidator<string>
{
    private readonly IUserChangeValidationService _service;
    private const int MaxEmailLength = 255;
    private const string ValidEmailPattern = @"[.\-_a-z0-9]+@([a-z0-9][\-a-z0-9]+\.)+[a-z]{2,6}";
    public EmailValidator(IUserChangeValidationService service)
    {
        _service = service;
        RuleFor(email => email)
            .NotEmpty().WithMessage("The email cannot be empty.")
            .MaximumLength(MaxEmailLength)
            .WithMessage($"The email should be no more than {MaxEmailLength}.")
            .EmailAddress().WithMessage("Invalid email address format.")
            .Matches(ValidEmailPattern, RegexOptions.IgnoreCase).WithMessage("Invalid email address format.")
            .MustAsync(CheckIsAvailableEmail).WithMessage("The email is already taken.");
    }
    private async Task<bool> CheckIsAvailableEmail(string email, CancellationToken cancellationToken)
    {
        return await _service.IsAvailableEmailAsync(email, cancellationToken);// это должно выполняться синхронно так как FluentValidation не поддерживает синхронные вызовы
    }


}