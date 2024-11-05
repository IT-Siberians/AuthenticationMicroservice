using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Contracts;
using System.Security.Claims;
using WebApiAuthenticate.Requests;

namespace WebApiAuthenticate.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController(
    IUserManagementService managementService,
    IUserValidationService validationService) : ControllerBase
{
    [HttpPost("Login")]
    public async Task<ActionResult> Login(UserLoginRequest request, CancellationToken cancellationToken)
    {
        var user = await managementService.GetUserByLoginAsync(request.Login, cancellationToken);
        if (user == null)
            return Unauthorized("Invalid pair login and password");

        var validationModel = new ValidatePasswordModel(user.Id, request.Password);
        var isValidPassword = await validationService.ValidatePasswordAsync(validationModel, cancellationToken);
        if (!isValidPassword)
            return Unauthorized("Invalid pair login and password");

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Username),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.AccountStatus.ToString())
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

        return NoContent();
    }

    [Authorize]
    [HttpPost("Logout")]
    public async Task<ActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return NoContent();
    }
}