using Domain.ValueObjects.BaseEntities;
using Domain.ValueObjects.Exceptions.PasswordHashExceptions;
using static Common.Helpers.Constants.PasswordHashConstants;

namespace Domain.ValueObjects.ValueObjects;

/// <summary>
/// Базовый элемент Хэшированный пароль
/// </summary>
/// <param name="value">Строка хранящаяся в элементе и проходящая валидацию на соответствие правилам Хэшированного пароля</param>
public class PasswordHash(string value) : ValueObject<string>(value)
{
    /// <summary>
    /// Проверяет строку на соответствие формату Хэшированного пароля
    /// </summary>
    /// <param name="value">Строка хранящаяся в элементе и проходящая валидацию на соответствие правилам хэшированного пароля</param>
    /// <exception cref="ArgumentNullException">Нулевая или пустая строка в параметрах метода</exception>
    protected override void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new PasswordHashEmptyException(value);

        if (value.Length > PASSWORDHASH_MAX_LENGTH)
            throw new PasswordMaxLengthException(value.Length, nameof(value));
    }
}