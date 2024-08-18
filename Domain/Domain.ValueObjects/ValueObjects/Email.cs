using Domain.ValueObjects.BaseEntities;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects.ValueObjects;

/// <summary>
/// Базовый элемент Электронная почта
/// </summary>
/// <param name="value">Строка хранящаяся в элементе и проходящая валидацию на соответствие правилам Электронной почты</param>
public class Email(string value) : ValueObject<string>(value)
{
    private const int MaxEmailLength = 255;
    private const string ValidEmailPattern = @"[.\-_a-z0-9]+@([a-z0-9][\-a-z0-9]+\.)+[a-z]{2,6}";

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
            throw new ArgumentNullException(nameof(value), "Email cannot null or empty");

        if (value.Length > MaxEmailLength)
            throw new ArgumentOutOfRangeException(
                nameof(value),
                message:$"Invalid email address  length. Maximum length is {MaxEmailLength}. Email value: {value}");

        var isMatch = Regex.Match(value, ValidEmailPattern, RegexOptions.IgnoreCase);
        if (!isMatch.Success)
            throw new FormatException(
                $"Invalid email address format. Email value: {value}");
    }
}