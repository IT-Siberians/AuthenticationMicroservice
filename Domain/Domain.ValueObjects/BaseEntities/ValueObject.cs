namespace Domain.ValueObjects.BaseEntities;
public abstract class ValueObject<T>
{
    public T Value { get; }
    /// <summary>
    /// Базовый элемент системы
    /// </summary>
    /// <param name="value">Значение, которое хранится в элементе и проходит валидацию</param>
    protected ValueObject(T value)
    {
        Validate(value);
        Value = value;
    }
    /// <summary>
    /// Метод валидации
    /// </summary>
    /// <param name="value">Значение, которое хранится в элементе и проходит валидацию</param>
    protected abstract void Validate(T value);
}