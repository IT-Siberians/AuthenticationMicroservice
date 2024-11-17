namespace WebApiAuthenticate.Helpers;

/// <summary>
/// Класс, содержащий стандартные сообщения об ошибках, используемые в процессе авторизации.
/// Эти сообщения используются для информирования пользователя о различных проблемах, таких как отказ в доступе или недействительные данные.
/// </summary>
public static class CustomAuthorizationMessages
{
    /// <summary>
    /// Стандартное сообщение об ошибке авторизации, которое возвращается в случае отказа в доступе.
    /// </summary>
    public const string STANDART_ACCESS_DENIED_ERROR_MESSAGE = "Authorization failed.";

    /// <summary>
    /// Сообщение об ошибке, если пользователь не аутентифицирован и не может получить доступ к защищенному ресурсу.
    /// </summary>
    public const string STANDART_USER_NOT_AUTHENTICATION_ERROR_MESSAGE = "User is not authenticated.";

    /// <summary>
    /// Сообщение об ошибке, если у пользователя отсутствует валидный идентификатор. Это сообщение используется, когда идентификатор не найден или он некорректен.
    /// </summary>
    public const string USER_HAVE_NOT_ID_ERROR_MESSAGE = "User does not have a valid identifier.";

    /// <summary>
    /// Сообщение об ошибке, если у ресурса отсутствует идентификатор или идентификатор является недействительным.
    /// Используется, когда невозможно получить доступ к ресурсу из-за отсутствия или некорректности идентификатора.
    /// </summary>
    public const string RESOURSE_HAVE_NOT_ID_ERROR_MESSAGE = "Invalid or missing resource ID.";

    /// <summary>
    /// Сообщение об ошибке, если текущий пользователь не является владельцем ресурса, к которому он пытается получить доступ.
    /// Это сообщение используется в случаях, когда пользователь не имеет прав для выполнения операции над данным ресурсом.
    /// </summary>
    public const string USER_IS_NOT_OWNER_ERROR_MESSAGE = "User is not the owner of this resource.";

    /// <summary>
    /// Сообщение об ошибке, если контекст ресурса является недействительным. Это может означать, что ресурс не существует или имеет некорректные данные.
    /// </summary>
    public const string INVALID_CONTEXT_RESOURSE_ERROR_MESSAGE = "Invalid context resource.";
}