using Domain.ValueObjects.BaseEntities;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects.ValueObjects;
public class Email : ValueObject<string>
{
    /// <summary>
    /// Конструктор для EF
    /// </summary>
    protected Email() : base(string.Empty)
    {

    }
    /// <summary>
    /// Базовый элемент Электронная почта
    /// </summary>
    /// <param name="value">Строка хранящаяся в элементе и проходящая валидацию на соответствие правилам Электронной почты</param>

    public Email(string value) : base(value)
    {

    }
    public const int MaxEmailLength = 255;//TODO:выделить в отдельные статические файлы
    private const string ValidEmailPattern = @"[.\-_a-z0-9]+@([a-z0-9][\-a-z0-9]+\.)+[a-z]{2,6}";//TODO:выделить в отдельные статические файлы
    /// <summary>
    /// Метод проверки соответствия правилам базовой электронной почты
    /// </summary>
    /// <param name="value">Строка хранящаяся в элементе и проходящая валидацию на соответствие правилам Электронной почты</param>
    /// <exception cref="ArgumentNullException">Нулевая или пустая строка в параметрах метода</exception>
    /// <exception cref="ArgumentOutOfRangeException">Несоответствие длине электронной почты</exception>
    /// <exception cref="ArgumentException">Несоответствие паттерну электронной почты</exception>
    protected override void Validate(string? value)
    {
        if ((value == null) ||
            (string.IsNullOrWhiteSpace(value)))
            throw new ArgumentNullException(nameof(value), "Email cannot null or empty");

        if (value.Length > MaxEmailLength)
            throw new ArgumentOutOfRangeException(
                $"Invalid email address  length. Maximum length is {MaxEmailLength}. Email value: {value}");

        var isMatch = Regex.Match(value, ValidEmailPattern, RegexOptions.IgnoreCase); //TODO:выделить в отдельные статические файлы
        if (!isMatch.Success)
            throw new ArgumentException(
                $"Invalid email address format. Email value: {value}"); //TODO:FormatException или кастомные исключения
    }
}