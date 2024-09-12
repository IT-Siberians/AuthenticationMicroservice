using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Contracts;
using WebApiAuthenticate.Requests;

namespace WebApiAuthenticate.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class ConfirmsController(
        IUserManagementService managementService,
        IMapper mapper,
        IUserValidationService validationService) : ControllerBase
    {
        [HttpPatch("ConfirmEmail")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<ActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest request, CancellationToken cancellationToken)
        {
            var isLinkExpired = await validationService.IsLinkExpiredAsync(request.CreatedDateTime, cancellationToken);
            if (!isLinkExpired)
                return BadRequest("Link is Expired");

            var isAvailableEmail = await validationService.IsAvailableEmailAsync(request.NewEmail, cancellationToken);
            if (!isAvailableEmail)
            {
                return BadRequest("Email is reserved");
            }

            var userToUpdate = await managementService.GetUserByIdAsync(request.Id, cancellationToken);
            if (userToUpdate is null)
                return NotFound($"The user \"{request.Id}\" for the update does not exist");

            var confirmEmailModel = mapper.Map<SetUserEmailModel>(request);

            var updateResult = await managementService.SetUserEmailAsync(confirmEmailModel, cancellationToken);

            if (!updateResult)
                return NotFound();

            return NoContent();
        }
    }
}
