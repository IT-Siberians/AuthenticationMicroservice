using AutoMapper;
using Common.Helpers.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Otus.QueueDto.User;
using Services.Abstractions;
using Services.Contracts;
using WebApiAuthenticate.Requests;

namespace WebApiAuthenticate.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class ConfirmsController(
        IUserManagementService managementService,
        INotificationService notificationService,
        IMapper mapper,
        IUserValidationService validationService) : ControllerBase
    {
        [HttpPost("ConfirmEmail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<ActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest request, CancellationToken cancellationToken)
        {
            var isValidCode = await validationService.ValidateVerificationCodeAsync(request.Id, request.Code, cancellationToken);
            if (!isValidCode)
                return BadRequest("Invalid code verification");

            var userToUpdate = await managementService.GetUserByIdAsync(request.Id, cancellationToken);
            if (userToUpdate is null)
                return NotFound($"The user \"{request.Id}\" for the update does not exist");

            if (userToUpdate.Email != request.NewEmail)
            {
                var isAvailableEmail = await validationService.IsAvailableEmailAsync(request.NewEmail, cancellationToken);
                if (!isAvailableEmail)
                    return BadRequest("Email is reserved");
            }

            var confirmEmailModel = mapper.Map<EmailConfirmationModel>(request);
            var updateResult = await managementService.SetUserEmailAsync(confirmEmailModel, cancellationToken);

            if (updateResult is null)
                return NotFound();

            switch (userToUpdate.AccountStatus)
            {
                case AccountStatuses.UnconfirmedAccount:
                    await notificationService.NotifyChangeUserDataAsync(new UserSignUpEvent(
                            updateResult.Id,
                            updateResult.Username,
                            updateResult.Email,
                            updateResult.Firstname,
                            updateResult.Lastname),
                            cancellationToken);
                    break;
                case AccountStatuses.ConfirmedAccount:
                    await notificationService.NotifyChangeUserDataAsync(new EmailChangedEvent(
                            updateResult.Id,
                            updateResult.Email),
                            cancellationToken);
                    break;
                case AccountStatuses.RestrictedAccount:
                default:
                    return BadRequest();
            }

            return NoContent();
        }
    }
}