using Domain.ValueObjects.BaseEntities;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects.ValueObjects;
/// <summary>
/// Базовый элемент Имя пользователя(никнейм)
/// </summary>
/// <param name="value">Строка хранящаяся в элементе и проходящая валидацию на соответствие правилам Имени пользователя(никнейма)</param>
public class Username(string value) : ValueObject<string>(value)
{
    public const int MinNameLength = 3;
    public const int MaxNameLength = 30;
    private const string ValidNamePattern = "(^[a-zA-Z_-]+$)";
    /// <summary>
    /// Метод проверки соответствия правилам базового имени пользователя(никнейма)
    /// </summary>
    /// <param name="value"></param>
    /// <exception cref="ArgumentNullException">Нулевая или пустая строка в параметрах метода</exception>
    /// <exception cref="ArgumentOutOfRangeException">Несоответствие длине имени пользователя(никнейма)</exception>
    /// <exception cref="ArgumentException">Несоответствие паттерну имени пользователя(никнейму)</exception>
    protected override void Validate(string value)
    {
        if ((value == null) ||
            (string.IsNullOrWhiteSpace(value)))
            throw new ArgumentNullException(nameof(value), "Username cannot null or empty");

        if (value.Length > MaxNameLength)
            throw new ArgumentOutOfRangeException(nameof(value),
                $"Invalid username  length. Maximum length is {MaxNameLength}. Username value: {value}");

        if (value.Count(char.IsLetterOrDigit) < MinNameLength)
            throw new ArgumentOutOfRangeException(nameof(value),
            $"Invalid username  length. the minimum number of alphanumeric characters is equal to {MinNameLength}. Username value: {value}");
        
        var isMatch = Regex.Match(value, ValidNamePattern, RegexOptions.IgnoreCase);
        if (!isMatch.Success)
                throw new ArgumentException($"The username contains invalid characters. Username value: {value}");
    }
}