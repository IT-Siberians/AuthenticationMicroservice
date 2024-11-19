using Domain.ValueObjects.BaseEntities;

namespace Domain.ValueObjects.ValueObjects;

/// <summary>
/// Базовый элемент для имени пользователя.
/// </summary>
public class FirstName(string value) : UserNameBase(value)
{
    /// <summary>
    /// Валидация для имени пользователя.
    /// </summary>
    /// <param name="value">Значение имени.</param>
    protected override void Validate(string value)
    {
        ValidateBase(value, nameof(FirstName));
    }
}