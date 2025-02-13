using Domain.ValueObjects.BaseEntities;

namespace Domain.ValueObjects.ValueObjects;

/// <summary>
/// Базовый элемент для фамилии пользователя.
/// </summary>
public class LastName(string value) : UserNameBase(value)
{
    /// <summary>
    /// Валидация для фамилии пользователя.
    /// </summary>
    /// <param name="value">Значение фамилии.</param>
    protected override void Validate(string value)
    {
        ValidateBase(value, nameof(LastName));
    }
}