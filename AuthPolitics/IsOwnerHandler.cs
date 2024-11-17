using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace AuthPolitics;

public class IsOwnerHandler : AuthorizationHandler<IsOwnerRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsOwnerRequirement requirement)
    {
        var isAuthenticatedUser = context.User.Identity.IsAuthenticated;
        if (!isAuthenticatedUser)
        {
            context.Fail(new AuthorizationFailureReason(this, "Not Authenticate user"));
            return Task.CompletedTask;
        }

        var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
        {
            context.Fail(new AuthorizationFailureReason(this, "has not user id claim"));
            return Task.CompletedTask;
        }

        var userId = Guid.Parse(userIdClaim.Value);

        if (context.Resource is HttpContext httpContext)
        {
           
        }
    }
}