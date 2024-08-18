using Domain.ValueObjects.BaseEntities;

namespace Domain.ValueObjects.ValueObjects;

/// <summary>
/// Базовый элемент Хэшированный пароль
/// </summary>
/// <param name="value">Строка хранящаяся в элементе и проходящая валидацию на соответствие правилам Хэшированного пароля</param>
public class PasswordHash(string value) : ValueObject<string>(value)
{
    public const int MaxPasswordHashLength = 256;

    /// <summary>
    /// Метод проверки соответствия правилам базового хэшированного пароля
    /// </summary>
    /// <param name="value">Строка хранящаяся в элементе и проходящая валидацию на соответствие правилам хэшированного пароля</param>
    /// <exception cref="ArgumentNullException">Нулевая или пустая строка в параметрах метода</exception>
    protected override void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(value), "PasswordHash cannot null or empty");
    }
}