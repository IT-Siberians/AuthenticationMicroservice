namespace WebApiAuthenticate.Helpers;

/// <summary>
/// Класс, содержащий константы, которые используются для обработки и хранения информации
/// о статусе отказа в авторизации, а также для настройки ответа в формате JSON.
/// </summary>
public static class CustomAuthorizationConstants
{
    /// <summary>
    /// Ключ для хранения информации о причине отказа в авторизации в коллекции Items объекта HttpContext.
    /// Этот ключ используется для передачи причины отказа в авторизации между компонентами приложения.
    /// </summary>
    public const string FAILURE_REASON_ITEM_KEY = "AuthorizationFailureReason";

    /// <summary>
    /// Ключ для хранения информации о коде статуса ошибки отказа в авторизации в коллекции Items объекта HttpContext.
    /// Этот ключ используется для хранения HTTP статуса ошибки при отказе в авторизации.
    /// </summary>
    public const string FAILURE_STATUS_CODE_ITEM_KEY = "AuthorizationFailureStatusCode";

    /// <summary>
    /// Стандартный тип содержимого для ответа в формате JSON.
    /// Этот тип содержимого используется для указания формата ответа при отказе в авторизации.
    /// </summary>
    public const string RESPONSE_CONTENT_TYPE = "application/json";
}