namespace WebApiAuthenticate.Responses;

/// <summary>
/// Обертка для стандартного ответа API, включающая статус выполнения, данные и сообщение об ошибке.
/// Используется для унифицированного возвращения результатов из методов контроллеров.
/// </summary>
/// <typeparam name="T">Тип данных, которые будут возвращены в случае успешного выполнения операции.</typeparam>
public class ApiResponse<T>
{
    /// <summary>
    /// Указывает на успешность выполнения операции.
    /// </summary>
    /// <value>True, если операция прошла успешно; False, если произошла ошибка.</value>
    public bool Success { get; set; }

    /// <summary>
    /// Данные, возвращаемые в случае успешного выполнения операции.
    /// </summary>
    /// <value>Объект типа <typeparamref name="T"/> с результатом операции.</value>
    public T? Data { get; set; }

    /// <summary>
    /// Сообщение об ошибке, если операция не была успешной.
    /// </summary>
    /// <value>Сообщение об ошибке в виде строки.</value>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Конструктор для успешного ответа с данными.
    /// </summary>
    /// <param name="data">Данные, которые будут возвращены в случае успешного выполнения.</param>
    public ApiResponse(T? data)
    {
        Success = true;
        Data = data;
        ErrorMessage = null;
    }

    /// <summary>
    /// Конструктор для ответа с ошибкой.
    /// </summary>
    /// <param name="errorMessage">Сообщение об ошибке, которое будет возвращено в случае неудачи.</param>
    public ApiResponse(string? errorMessage)
    {
        Success = false;
        Data = default;
        ErrorMessage = errorMessage;
    }
}