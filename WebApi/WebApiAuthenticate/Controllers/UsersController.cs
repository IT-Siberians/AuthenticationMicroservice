using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Contracts;
using WebApiAuthenticate.Requests;
using WebApiAuthenticate.Responses;

namespace WebApiAuthenticate.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class UsersController(
    IUserManagementService managementService,
    IUserValidationService validationService,
    INotificationService notificationService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserResponse>))]
    public async Task<ActionResult<IEnumerable<UserResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var users = await managementService.GetAllUsersAsync(cancellationToken);
        return Ok(mapper.Map<IEnumerable<UserResponse>>(users));
    }

    [HttpGet("{id:guid}", Name = "GetUserById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<ActionResult<UserResponse>> GetUserById(Guid id, CancellationToken cancellationToken)
    {
        var user = await managementService.GetUserByIdAsync(id, cancellationToken);
        if (user == null)
            return NotFound($"The user with this id - \"{id}\" was not found");

        var userResponse = mapper.Map<UserResponse>(user);
        return Ok(userResponse);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    public async Task<ActionResult<UserResponse>> CreateUser([FromBody] CreatingUserRequest request, CancellationToken cancellationToken)
    {
        var isAvailableUsername = await validationService.IsAvailableUsernameAsync(request.Username, cancellationToken);
        if (!isAvailableUsername)
        {
            return BadRequest("Username is reserved.");
        }

        var isAvailableEmail = await validationService.IsAvailableEmailAsync(request.Email, cancellationToken);
        if (!isAvailableEmail)
        {
            return BadRequest("Email is reserved");
        }

        var createUserDto = mapper.Map<CreateUserModel>(request);
        var createdUser = await managementService.CreateUserAsync(createUserDto, cancellationToken);
        if (createdUser == null)
            return BadRequest("The user has not been created");

        var userResponse = mapper.Map<UserResponse>(createdUser);
        return CreatedAtAction(nameof(GetUserById), new { userResponse.Id }, userResponse);
    }

    [HttpPatch("{id:guid}/ChangeUsername")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<ActionResult> ChangeUsername(Guid id, [FromBody] NewUsernameRequest newUsername, CancellationToken cancellationToken)
    {
        var isAvailableUsername = await validationService.IsAvailableUsernameAsync(newUsername.UsernameValue, cancellationToken);
        if (!isAvailableUsername)
        {
            return BadRequest("Username is reserved.");
        }

        var userToUpdate = await managementService.GetUserByIdAsync(id, cancellationToken);
        if (userToUpdate is null)
            return NotFound($"The user \"{id}\" for the update does not exist");

        var changeUsernameModel = new ChangeUsernameModel()
        {
            Id = id,
            NewUsername = newUsername.UsernameValue
        };

        var updateResult = await managementService.ChangeUsernameAsync(changeUsernameModel, cancellationToken);
        if (!updateResult)
            return NotFound();

        return NoContent();
    }

    [HttpPatch("{id:guid}/ChangePassword")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<ActionResult> ChangePassword(Guid id, [FromBody] ChangePasswordRequest request, CancellationToken cancellationToken)
    {
        var validateModel = new ValidatePasswordModel() { Id = id, Password = request.OldPassword };
        var isOldPasswordValid = await validationService.ValidatePasswordAsync(validateModel, cancellationToken);
        if (!isOldPasswordValid)
        {
            return BadRequest("Old password not valid");
        }

        var userToUpdate = await managementService.GetUserByIdAsync(id, cancellationToken);
        if (userToUpdate is null)
            return NotFound($"The user \"{id}\" for the update does not exist");

        var changePasswordModel = new ChangePasswordModel()
        {
            Id = id,
            NewPassword = request.NewPassword
        };

        var updateResult = await managementService.ChangePasswordAsync(changePasswordModel, cancellationToken);
        if (!updateResult)
            return NotFound();

        return NoContent();
    }

    [HttpPost("{id:guid}/ChangeEmail")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<ActionResult> CreateEmailChangeRequest(Guid id, [FromBody] NewEmailRequest newEmail, CancellationToken cancellationToken)
    {
        var isAvailableEmail = await validationService.IsAvailableEmailAsync(newEmail.EmailValue, cancellationToken);
        if (!isAvailableEmail)
        {
            return BadRequest("Email is reserved");
        }

        var userToUpdate = await managementService.GetUserByIdAsync(id, cancellationToken);
        if (userToUpdate is null)
            return NotFound($"The user \"{id}\" for the update does not exist");

        var changeEmailModel = new MailConfirmationGenerationModel()
        {
            Id = id,
            NewEmail = newEmail.EmailValue
        };

        var isCreated = await notificationService.CreateSetEmailRequest(changeEmailModel, cancellationToken);
        if (isCreated)
        {
            return Ok($"A request has been created to change the email address to {changeEmailModel.NewEmail}. Check your email for confirmation.");
        }

        return BadRequest();
    }

    [HttpDelete("{id:guid}/Delete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<ActionResult<bool>> DeleteUser(Guid id, CancellationToken cancellationToken)
    {
        var userToDelete = await managementService.GetUserByIdAsync(id, cancellationToken);
        if (userToDelete is null)
            return NotFound($"The user \"{id}\" for the delete does not exist");

        var deleteResult = await managementService.DeleteUserSoftlyByIdAsync(id, cancellationToken);
        if (!deleteResult)
            return NotFound(deleteResult);

        return NoContent();
    }
}