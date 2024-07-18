using Domain.ValueObjects.Exceptions;

namespace Domain.ValueObjects.Validators;
public class PasswordHashValidator : Validator<string>
{
    public override bool Validate(string? value)
    {
        if (value == null)
        {
            ExceptionMessage = "PasswordHash cannot null";
            return false;
        }

        if (string.IsNullOrEmpty(value))
        {
            ExceptionMessage = "PasswordHash cannot empty";
            return false;
        }

        return true;
    }

    public override Exception GetValidationException() => new PasswordHashValidationException(ExceptionMessage);
}