using static Common.Helpers.EmailHelpers.EmailDomainMessages;

namespace Domain.ValueObjects.Exceptions.EmailExceptions;

/// <summary>
/// Исключительная ситуация создание Email с некорректным форматом
/// </summary>
/// <param name="emailValue">Значение создаваемого Email</param>
internal class EmailFormatException(string emailValue) 
    : FormatException(
        string.Format(EMAIL_FORMAT_ERROR, emailValue));