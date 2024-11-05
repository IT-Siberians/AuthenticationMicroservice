using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Contracts;
using WebApiAuthenticate.Requests;

namespace WebApiAuthenticate.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController(
    IUserManagementService managementService,
    IUserValidationService validationService,
    ITokenService tokenService) : ControllerBase
{
    [HttpPost("Login")]
    public async Task<ActionResult> Login(UserLoginRequest request, CancellationToken cancellationToken)
    {
        var user = await managementService.GetUserByLoginAsync(request.Login, cancellationToken);
        if (user == null)
            return NotFound("Invalid pair login and password");

        var validationModel = new ValidatePasswordModel(user.Id, request.Password);
        var isValidPassword = await validationService.ValidatePasswordAsync(validationModel, cancellationToken);
        if (!isValidPassword)
            return NotFound("Invalid pair login and password");

        var token = await tokenService.GetTokenAsync(user, cancellationToken);

        HttpContext.Response.Cookies.Append(token.TokenCookieName, token.Token);

        return NoContent();
    }
    [Authorize]
    [HttpPost("Logout")]
    public async Task<ActionResult> Logout()
    {
        HttpContext.Response.Cookies.Delete("auth");
        return NoContent();
    }
}