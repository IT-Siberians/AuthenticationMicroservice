using Domain.ValueObjects.BaseEntities;
using Domain.ValueObjects.Exceptions.EmailExceptions;
using System.Text.RegularExpressions;
using static Common.Helpers.Constants.EmailConstants;

namespace Domain.ValueObjects.ValueObjects;

/// <summary>
/// Базовый элемент Электронная почта
/// </summary>
/// <param name="value">Строка хранящаяся в элементе и проходящая валидацию на соответствие правилам Электронной почты</param>
public class Email(string value) : ValueObject<string>(value)
{
    private static readonly Regex ValidationRegex = new(EMAIL_VALID_PATTERN,
        RegexOptions.IgnoreCase | RegexOptions.Compiled);
    /// <summary>
    /// Проверяет строку на соответствие формату электронной почты
    /// </summary>
    /// <param name="value">Строка хранящаяся в элементе и проходящая валидацию на соответствие правилам Электронной почты</param>
    /// <exception cref="ArgumentNullException">Нулевая или пустая строка в параметрах метода</exception>
    /// <exception cref="ArgumentOutOfRangeException">Несоответствие длине электронной почты</exception>
    /// <exception cref="ArgumentException">Несоответствие паттерну электронной почты</exception>
    protected override void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new EmailEmptyException(nameof(value));

        if (value.Length > EMAIL_MAX_LENGTH)
            throw new EmailMaxLengthException(value.Length, nameof(value));

        var match = ValidationRegex.Match(value);
        if (!match.Success)
            throw new EmailFormatException(value);
    }
}