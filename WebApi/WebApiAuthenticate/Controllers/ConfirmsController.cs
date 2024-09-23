using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace WebApiAuthenticate.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class ConfirmsController(
        IUserManagementService managementService,
        IUserValidationService validationService,
        ILinkGeneratorService linkGeneratorService) : ControllerBase
    {
        [HttpGet("ConfirmEmail")]//работает только с get, не понял как сгенерированную ссылку отправить с конкретным http методом, типо если клиент переходит, это надо либо на gateway уже преобразовывать либо мб еще можно метод поделить, где с гета на пост или патч уходмт
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<ActionResult> ConfirmEmail(string link, CancellationToken cancellationToken)
        {
            var confirmEmailModel = await linkGeneratorService.GetModelFromLink(link, cancellationToken);
            if (confirmEmailModel is null)
                return NotFound();

            var isLinkExpired = await validationService.IsLinkExpiredAsync(confirmEmailModel.ExpirationDateTime, cancellationToken);
            if (isLinkExpired)
                return BadRequest("Link is Expired");

            var isAvailableEmail = await validationService.IsAvailableEmailAsync(confirmEmailModel, cancellationToken);
            if (!isAvailableEmail)
            {
                return BadRequest("Email is reserved");
            }

            var userToUpdate = await managementService.GetUserByIdAsync(confirmEmailModel.Id, cancellationToken);
            if (userToUpdate is null)
                return NotFound($"The user \"{confirmEmailModel.Id}\" for the update does not exist");

            var updateResult = await managementService.SetUserEmailAsync(confirmEmailModel, cancellationToken);

            if (!updateResult)
                return NotFound();

            return NoContent();
        }
    }
}
