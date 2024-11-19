using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using static WebApiAuthenticate.Helpers.CustomAuthorizationConstants;
using static WebApiAuthenticate.Helpers.CustomAuthorizationMessages;

namespace WebApiAuthenticate.Middlewares.AuthorizationMiddlewares.AuthorizePolitics.IsOwner;

/// <summary>
/// Обработчик авторизации для проверки, является ли текущий пользователь владельцем ресурса.
/// </summary>
public class IsOwnerHandler : AuthorizationHandler<IsOwnerRequirement>
{
    /// <summary>
    /// Выполняет проверку требования авторизации для владельца ресурса.
    /// </summary>
    /// <param name="context">Контекст авторизации, который содержит пользователя и информацию о ресурсе.</param>
    /// <param name="requirement">Требование авторизации, которое проверяется.</param>
    /// <returns>Задача, представляющая асинхронную операцию.</returns>
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsOwnerRequirement requirement)
    {
        var isAuthenticatedUser = context.User.Identity is { IsAuthenticated: true };
        if (!isAuthenticatedUser)
        {
            SetFailureReason(context, STANDART_USER_NOT_AUTHENTICATION_ERROR_MESSAGE, StatusCodes.Status401Unauthorized);
            return Task.CompletedTask;
        }

        var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
        {
            SetFailureReason(context, USER_HAVE_NOT_ID_ERROR_MESSAGE, StatusCodes.Status403Forbidden);
            return Task.CompletedTask;
        }

        var currentUserId = Guid.Parse(userIdClaim.Value);

        if (context.Resource is HttpContext resource)
        {
            var idString = resource.Request.RouteValues["id"]?.ToString();

            if (string.IsNullOrEmpty(idString) || !Guid.TryParse(idString, out var idFromRoute))
            {
                SetFailureReason(context, RESOURSE_HAVE_NOT_ID_ERROR_MESSAGE, StatusCodes.Status403Forbidden);
                return Task.CompletedTask;
            }

            if (currentUserId != idFromRoute)
            {
                SetFailureReason(context, USER_IS_NOT_OWNER_ERROR_MESSAGE, StatusCodes.Status403Forbidden);
                return Task.CompletedTask;
            }

            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        SetFailureReason(context, INVALID_CONTEXT_RESOURSE_ERROR_MESSAGE, StatusCodes.Status403Forbidden);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Устанавливает причину отказа в авторизации и код состояния для ответа.
    /// </summary>
    /// <param name="context">Контекст авторизации.</param>
    /// <param name="reason">Причина отказа.</param>
    /// <param name="statusCode">Код состояния HTTP, соответствующий ошибке.</param>
    private void SetFailureReason(AuthorizationHandlerContext context, string reason, int statusCode)
    {
        if (context.Resource is HttpContext httpContext)
        {
            httpContext.Items[FAILURE_REASON_ITEM_KEY] = reason;
            httpContext.Items[FAILURE_STATUS_CODE_ITEM_KEY] = statusCode;
        }

        context.Fail();
    }
}