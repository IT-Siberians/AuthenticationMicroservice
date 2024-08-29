using System.Text.RegularExpressions;
using Domain.ValueObjects.BaseEntities;
using Domain.ValueObjects.Exceptions.UsernameExceptions;
using static Common.Helpers.UsernameHelpers.UsernameConstants;

namespace Domain.ValueObjects.ValueObjects;

/// <summary>
/// Базовый элемент Имя пользователя(никнейм)
/// </summary>
/// <param name="value">Строка хранящаяся в элементе и проходящая валидацию на соответствие правилам Хэшированного пароля</param>
public class Username(string value) : ValueObject<string>(value)
{
    private static readonly Regex ValidationRegex = new Regex(USERNAME_VALID_PATTERN,
        RegexOptions.IgnoreCase | RegexOptions.Compiled);
    /// <summary>
    /// Проверяет строку на соответствие формату  имени пользователя(никнейма)
    /// </summary>
    /// <param name="value"></param>
    /// <exception cref="ArgumentNullException">Нулевая или пустая строка в параметрах метода</exception>
    /// <exception cref="ArgumentOutOfRangeException">Несоответствие длине имени пользователя(никнейма)</exception>
    /// <exception cref="ArgumentException">Несоответствие паттерну имени пользователя(никнейму)</exception>
    protected override void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new UsernameEmptyException(nameof(value));

        switch (value.Length)
        {
            case > USERNAME_MAX_LENGTH:
                throw new UsernameMaxLengthException(value.Length, nameof(value));
            case < USERNAME_MIN_LENGTH:
                throw new UsernameMinLengthException(value.Length, nameof(value));
        }

        var match = ValidationRegex.Match(value);
        if (!match.Success)
            throw new UsernameInvalidCharacterException(value); ;
    }
}