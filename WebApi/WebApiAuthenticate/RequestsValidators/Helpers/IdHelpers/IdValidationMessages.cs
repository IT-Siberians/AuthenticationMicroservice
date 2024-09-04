namespace WebApiAuthenticate.RequestsValidators.Helpers.IdHelpers;

/// <summary>
/// Статический класс содержащий константы ошибок исключений в Id в валидации Request
/// </summary>
public static class IdValidationMessages
{
    /// <summary>
    /// Текст ошибки при попытке передать пустое значение в Id
    /// </summary>
    public const string ID_EMPTY_ERROR = 
        "The id cannot be empty.";
}