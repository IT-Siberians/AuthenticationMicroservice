using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Contracts;
using WebApiAuthenticate.Requests;

namespace WebApiAuthenticate.Controllers
{
    [Controller]
    [Route("/api/v1/[controller]")]
    public class ConfirmsController(
        IUserManagementService managementService,
        IMapper mapper,
        IConfirmLinkService linkService,
        IUserValidationService validationService) : Controller
    {
        [HttpGet("ConfirmEmail")]
        public async Task<ActionResult> ConfirmEmail(string token, string data, CancellationToken cancellationToken)
        {
            var codeModel = await linkService.GetDataFromLinkParameters<VerificationCodeModel>(token);

            var isValidModel = await validationService.ValidateLinkTokenAsync(codeModel, cancellationToken);
            if (!isValidModel)
            {
                ViewBag.IsValidModel = !isValidModel;
                return View("InvalidToken");
            }

            var emailConfirmationModel = await linkService.GetDataFromLinkParameters<EmailConfirmationModel>(data);
            ViewBag.EmailConfirmationModel = emailConfirmationModel;
            return View("ValidToken");
        }

        [HttpPatch("ConfirmEmail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<ActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest request, CancellationToken cancellationToken)
        {
            var isAvailableEmail = await validationService.IsAvailableEmailAsync(request.NewEmail, cancellationToken);
            if (!isAvailableEmail)
            {
                return BadRequest("Email is reserved");
            }

            var userToUpdate = await managementService.GetUserByIdAsync(request.Id, cancellationToken);
            if (userToUpdate is null)
                return NotFound($"The user \"{request.Id}\" for the update does not exist");

            var confirmEmailModel = mapper.Map<EmailConfirmationModel>(request);

            var updateResult = await managementService.SetUserEmailAsync(confirmEmailModel, cancellationToken);

            if (!updateResult)
                return NotFound();

            return Ok("The email has been successfully confirmed");
        }
    }
}
