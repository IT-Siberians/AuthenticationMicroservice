using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Contracts;
using WebApiAuthenticate.Requests;
using WebApiAuthenticate.Responses;

namespace WebApiAuthenticate.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController(
    IUserManagementService managementService,
    IUserValidationService validationService,
    IAuthManagerService authManagerService) : ControllerBase
{
    [HttpPost("Login")]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(ApiResponse<Guid>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<string>))]
    public async Task<IActionResult> Login(UserLoginRequest request, CancellationToken cancellationToken)
    {
        var user = await managementService.GetUserByLoginAsync(request.Login, cancellationToken);
        if (user == null)
            return BadRequest(new ApiResponse<string>("Invalid pair login and password"));

        var validationModel = new ValidatePasswordModel(user.Id, request.Password);
        var isValidPassword = await validationService.ValidatePasswordAsync(validationModel, cancellationToken);
        if (!isValidPassword)
            return BadRequest(new ApiResponse<string>("Invalid pair login and password"));

        var claimsPrincipal = authManagerService.BuildClaimsPrincipal(user);

        await HttpContext.SignInAsync(
            claimsPrincipal.Identity!.AuthenticationType,
            claimsPrincipal);

        return Ok(new ApiResponse<Guid>(user.Id));
    }

    [Authorize]
    [HttpPost("Logout")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
    public async Task<IActionResult> Logout()
    {
        var scheme = authManagerService.GetAuthScheme(HttpContext);

        await HttpContext.SignOutAsync(scheme);

        return NoContent();
    }
}