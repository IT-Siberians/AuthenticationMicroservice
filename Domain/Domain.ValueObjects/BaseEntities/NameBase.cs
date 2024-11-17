using System.Globalization;
using System.Text.RegularExpressions;
using Domain.ValueObjects.Exceptions.NameExceptions;
using static Common.Helpers.Constants.NameConstants;

namespace Domain.ValueObjects.BaseEntities;

/// <summary>
/// Базовый класс для элементов с валидацией имени или фамилии.
/// </summary>
public abstract class UserNameBase(string value) : ValueObject<string>(FormatValue(value))
{
    private static readonly Regex CharacterSetRegex =
        new Regex(NAME_CHARACHTER_SET_PATTERN, RegexOptions.IgnoreCase | RegexOptions.Compiled);

    private static readonly Regex CapitalizationRegex =
        new Regex(NAME_CAPITALIZATION_PATTERN, RegexOptions.Compiled);

    /// <summary>
    /// Форматирует строку перед сохранением.
    /// </summary>
    /// <param name="value">Строка, которую нужно отформатировать.</param>
    /// <returns>Отформатированная строка.</returns>
    private static string FormatValue(string value)
        => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.Trim());

    /// <summary>
    /// Основной метод валидации имени или фамилии.
    /// </summary>
    /// <param name="value">Значение, которое нужно проверить.</param>
    /// <exception cref="NameIsEmptyException">Если значение пустое.</exception>
    /// <exception cref="NameMaximumLengthException">Если значение слишком длинное.</exception>
    /// <exception cref="NameFormatException">Если формат значения неверен.</exception>
    protected void ValidateBase(string value, string name)
    {
        switch (value)
        {
            case null or { Length: 0 }:
                throw new NameIsEmptyException(name, nameof(value));
            case { Length: > NAME_MAX_LENGTH }:
                throw new NameMaximumLengthException(name, value.Length, nameof(value));
        }

        if (!CharacterSetRegex.IsMatch(value))
            throw new NameFormatException(name, value, NAME_CHARACHTER_SET_PATTERN);

        if (!CapitalizationRegex.IsMatch(value))
            throw new NameFormatException(name, value, NAME_CAPITALIZATION_PATTERN);
    }
}