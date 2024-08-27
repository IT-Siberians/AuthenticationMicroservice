using System.Text.RegularExpressions;
using Domain.ValueObjects.Exceptions.UsernameExceptions;

namespace Domain.ValueObjects.ValueObjects;

/// <summary>
/// Базовый элемент Имя пользователя(никнейм)
/// </summary>
/// <param name="value">Строка хранящаяся в элементе и проходящая валидацию на соответствие правилам Хэшированного пароля</param>
public class Username(string value) : ValueObject<string>(value)
{
    private const int MIN_USERNAME_LENGTH = 3;
    private const int MAX_USERNAME_LENGTH = 30;
    private const string VALID_USERNAME_PATTERN = "(^[a-zA-Z_-]+$)";

    /// <summary>
    /// Метод проверки соответствия правилам базового имени пользователя(никнейма)
    /// </summary>
    /// <param name="value"></param>
    /// <exception cref="ArgumentNullException">Нулевая или пустая строка в параметрах метода</exception>
    /// <exception cref="ArgumentOutOfRangeException">Несоответствие длине имени пользователя(никнейма)</exception>
    /// <exception cref="ArgumentException">Несоответствие паттерну имени пользователя(никнейму)</exception>
    protected override void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new UsernameEmptyException(value);

        switch (value.Length)
        {
            case > MAX_USERNAME_LENGTH:
                throw new UsernameMaxLengthException(value.Length);
            case < MIN_USERNAME_LENGTH:
                throw new UsernameMinLengthException(value.Length);
        }

        var match = Regex.Match(value, VALID_USERNAME_PATTERN, RegexOptions.IgnoreCase);
        if (!match.Success)
            throw new UsernameInvalidCharacterException();
    }
}