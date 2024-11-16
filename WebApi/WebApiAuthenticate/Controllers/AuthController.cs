using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Contracts;
using Services.Implementations;
using WebApiAuthenticate.Requests;

namespace WebApiAuthenticate.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController(
    IUserManagementService managementService,
    IUserValidationService validationService) : ControllerBase
{
    [HttpPost("Login")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    public async Task<ActionResult> Login(UserLoginRequest request, CancellationToken cancellationToken)
    {
        var user = await managementService.GetUserByLoginAsync(request.Login, cancellationToken);
        if (user == null)
            return BadRequest("Invalid pair login and password");

        var validationModel = new ValidatePasswordModel(user.Id, request.Password);
        var isValidPassword = await validationService.ValidatePasswordAsync(validationModel, cancellationToken);
        if (!isValidPassword)
            return BadRequest("Invalid pair login and password");

        var claimsPrincipal =
            new ClaimsPrincipalBuilder(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddIdentifier(user.Id.ToString())
                .AddUsername(user.Username)
                .AddEmail(user.Email)
                .AddAccountStatus(user.AccountStatus.ToString())
                .Build();

        await HttpContext.SignInAsync(
            claimsPrincipal.Identity!.AuthenticationType,
            claimsPrincipal);

        await HttpContext.SignInAsync(claimsPrincipal.Identity!.AuthenticationType, claimsPrincipal);

        return NoContent();
    }

    [Authorize]
    [HttpPost("Logout")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return NoContent();
    }
}