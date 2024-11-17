using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Contracts;
using WebApiAuthenticate.Requests;
using WebApiAuthenticate.Responses;

namespace WebApiAuthenticate.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class ConfirmsController(
        IUserManagementService managementService,
        INotificationEventFactory factory,
        IMapper mapper,
        IUserValidationService validationService) : ControllerBase
    {
        [HttpPost("ConfirmEmail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest request, CancellationToken cancellationToken)
        {
            var isValidCode = await validationService.ValidateVerificationCodeAsync(request.Id, request.Code, cancellationToken);
            if (!isValidCode)
                return BadRequest(new ApiResponse<string>("Invalid code verification"));

            var userToUpdate = await managementService.GetUserByIdAsync(request.Id, cancellationToken);
            if (userToUpdate is null)
                return NotFound(new ApiResponse<string>($"The user \"{request.Id}\" for the update does not exist"));

            if (userToUpdate.Email != request.NewEmail)
            {
                var isAvailableEmail = await validationService.IsAvailableEmailAsync(request.NewEmail, cancellationToken);
                if (!isAvailableEmail)
                    return BadRequest(new ApiResponse<string>("Email is reserved"));
            }

            var confirmEmailModel = mapper.Map<EmailConfirmationModel>(request);
            var updateResult = await managementService.SetUserEmailAsync(confirmEmailModel, cancellationToken);

            if (updateResult is null)
                return NotFound(new ApiResponse<string>("Updated user is null"));

            var notificationEvent = await factory.CreateNotificationEventAsync(updateResult, cancellationToken);
            await notificationEvent.NotifyAsync(cancellationToken);

            return NoContent();
        }
    }
}