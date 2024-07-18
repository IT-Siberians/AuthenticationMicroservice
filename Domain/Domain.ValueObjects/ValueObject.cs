using Domain.ValueObjects.Exceptions;

namespace Domain.ValueObjects;
public abstract class ValueObject<T>
{
    public T Value { get; }

    protected ValueObject(Validator<T> validator, T value)
    {
        if (validator == null)
            throw new ValidatorNotSpecifiedException(this);
        if (!validator.Validate(value))
            throw validator.GetValidationException();
        Value = value;
    }
}