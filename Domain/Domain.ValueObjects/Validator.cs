namespace Domain.ValueObjects;
public abstract class Validator<T>
{
    protected string ExceptionMessage = string.Empty;
    public abstract bool Validate(T value);
    public abstract Exception GetValidationException();
}