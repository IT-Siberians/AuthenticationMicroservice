namespace Domain.ValueObjects.BaseEntities;
public abstract class ValueObject<T>
{
    public T Value { get; }
    protected ValueObject(T value)
    {
        Validate(value);
        Value = value;
    }
    protected abstract void Validate(T value);
}