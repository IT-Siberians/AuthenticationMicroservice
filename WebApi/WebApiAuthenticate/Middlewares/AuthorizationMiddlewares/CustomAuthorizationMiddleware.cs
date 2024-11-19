using WebApiAuthenticate.Responses;
using static WebApiAuthenticate.Helpers.CustomAuthorizationConstants;
using static WebApiAuthenticate.Helpers.CustomAuthorizationMessages;

namespace WebApiAuthenticate.Middlewares.AuthorizationMiddlewares;

/// <summary>
/// Middleware для обработки авторизации и возврата соответствующего ответа в случае отказа в доступе.
/// Этот middleware перехватывает ответы с кодами статусов 401 (Unauthorized) и 403 (Forbidden),
/// и возвращает структурированный JSON ответ с информацией об ошибке авторизации.
/// </summary>
public class CustomAuthorizationMiddleware(RequestDelegate next)
{
    /// <summary>
    /// Метод для обработки HTTP-запроса и генерации ответа.
    /// Перехватывает ошибки авторизации с кодами статуса 401 и 403 и возвращает структурированный JSON-ответ.
    /// </summary>
    /// <param name="context">Контекст HTTP-запроса, содержащий информацию о текущем запросе и ответе.</param>
    /// <returns>Асинхронная задача.</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        await next(context);

        if (context.Response.StatusCode is StatusCodes.Status401Unauthorized or StatusCodes.Status403Forbidden &&
            !context.Response.HasStarted)
        {
            var errorMessage = context.Items[FAILURE_REASON_ITEM_KEY] as string;
            var statusCode = context.Items[FAILURE_STATUS_CODE_ITEM_KEY] as int? ?? context.Response.StatusCode;

            context.Response.ContentType = RESPONSE_CONTENT_TYPE;
            context.Response.StatusCode = statusCode;

            var errorResponse = errorMessage ?? STANDART_ACCESS_DENIED_ERROR_MESSAGE;

            var response = new ApiResponse<string>(errorResponse);

            await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
        }
    }
}