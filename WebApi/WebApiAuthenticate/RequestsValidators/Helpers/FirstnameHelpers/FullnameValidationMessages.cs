namespace WebApiAuthenticate.RequestsValidators.Helpers.FirstnameHelpers;

public static class FullnameValidationMessages
{
    /// <summary>
    /// Текст ошибки при попытке передать пустую строку в Firstname
    /// </summary>
    public const string FULLNAME_EMPTY_ERROR =
        "The first or last name cannot be empty.";

    /// <summary>
    /// Текст ошибки при попытке передать строку в Firstname с некорректной длиной
    /// </summary>
    public const string FULLNAME_MAX_LENGTH_ERROR =
        "The first or last name cannot be less than {0} characters";

    /// <summary>
    /// Текст ошибки при попытке передать строку в Firstname в недопустимом формате
    /// </summary>
    public const string FULLNAME_FORMAT_ERROR =
        "Invalid first or last name format.";
}
