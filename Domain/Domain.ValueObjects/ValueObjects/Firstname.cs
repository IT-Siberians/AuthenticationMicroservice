using Domain.ValueObjects.BaseEntities;
using Domain.ValueObjects.Exceptions.UserFullNameExceptions;
using System.Globalization;
using System.Text.RegularExpressions;
using static Common.Helpers.Constants.UserFullnameConstants;

namespace Domain.ValueObjects.ValueObjects;

/// <summary>
/// Базовый элемент для имени пользователя.
/// </summary>
/// <param name="value">Строка хранящаяся в элементе и проходящая валидацию на соответствие правилам Имя пользователя</param>
public class Firstname(string value) : ValueObject<string>(FormatValue(value))
{
    private static readonly Regex CharacterSetRegex =
        new Regex(USER_FULLNAME_CHARACHTER_SET_PATTERN, RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private static readonly Regex CapitalizationRegex =
        new Regex(USER_FULLNAME_CAPITALIZATION_PATTERN, RegexOptions.Compiled);

    /// <summary>
    /// Метод для форматирования значения перед валидацией и сохранением.
    /// </summary>
    /// <param name="value">Значение имени, которое требуется форматировать</param>
    /// <returns>Отформатированное значение имени</returns>
    private static string FormatValue(string value)
        =>CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.Trim());

    /// <summary>
    /// Метод для валидации ValueObject - имя.
    /// </summary>
    /// <param name="value">Значение имени.</param>
    /// <exception cref="UserFullNameIsEmptyException">Выбрасывается, если имя пустое или состоит только из пробелов.</exception>
    /// <exception cref="UserFullNameMaximumLengthException">Выбрасывается, если длина имени превышает максимально допустимую.</exception>
    /// <exception cref="UserFullNameFormatException">Выбрасывается, если имя не соответствует установленному формату.</exception>
    protected override void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new UserFullNameIsEmptyException(
                nameof(Firstname),
                nameof(value));
        if (value.Length > USER_FULLNAME_MAX_LENGTH)
            throw new UserFullNameMaximumLengthException(
                nameof(Firstname),
                value.Length,
                nameof(value));

        var matchCharacterSet = CharacterSetRegex.Match(value);
        var matchCapitalization = CapitalizationRegex.Match(value);

        if (!matchCharacterSet.Success)
            throw new UserFullNameFormatException(
                nameof(Firstname),
                value,
                USER_FULLNAME_CHARACHTER_SET_PATTERN);
        if (!matchCapitalization.Success)
            throw new UserFullNameFormatException(
                nameof(Firstname),
                value,
                USER_FULLNAME_CAPITALIZATION_PATTERN);
    }
}