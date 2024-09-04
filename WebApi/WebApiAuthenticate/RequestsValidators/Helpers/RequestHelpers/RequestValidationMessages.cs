namespace WebApiAuthenticate.RequestsValidators.Helpers.RequestHelpers;

/// <summary>
/// Статический класс содержащий константы ошибок исключений в валидации Request
/// </summary>
public static class RequestValidationMessages
{
    /// <summary>
    /// Текст ошибки при попытке передать null в Request
    /// </summary>
    public const string REQUEST_EMPTY_ERROR = 
        "Request should not be empty.";
}