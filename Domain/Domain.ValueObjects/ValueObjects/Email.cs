using Domain.ValueObjects.BaseEntities;
using System.Text.RegularExpressions;
using Domain.ValueObjects.Exceptions.EmailExceptions;

namespace Domain.ValueObjects.ValueObjects;

/// <summary>
/// Базовый элемент Электронная почта
/// </summary>
/// <param name="value">Строка хранящаяся в элементе и проходящая валидацию на соответствие правилам Электронной почты</param>
public class Email(string value) : ValueObject<string>(value)
{
    private const int MAX_EMAIL_LENGTH = 255;
    private const string VALID_EMAIL_PATTERN = @"[.\-_a-z0-9]+@([a-z0-9][\-a-z0-9]+\.)+[a-z]{2,6}";

    /// <summary>
    /// Метод проверки соответствия правилам базовой электронной почты
    /// </summary>
    /// <param name="value">Строка хранящаяся в элементе и проходящая валидацию на соответствие правилам Электронной почты</param>
    /// <exception cref="ArgumentNullException">Нулевая или пустая строка в параметрах метода</exception>
    /// <exception cref="ArgumentOutOfRangeException">Несоответствие длине электронной почты</exception>
    /// <exception cref="ArgumentException">Несоответствие паттерну электронной почты</exception>
    protected override void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new EmailEmptyException(value);

        if (value.Length > MAX_EMAIL_LENGTH)
            throw new EmailMaxLengthException(value.Length);

        var match = Regex.Match(value, VALID_EMAIL_PATTERN, RegexOptions.IgnoreCase);
        if (!match.Success)
            throw new EmailFormatException();
    }
}