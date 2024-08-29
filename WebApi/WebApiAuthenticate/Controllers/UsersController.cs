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
    IUserChangeValidationService validationService,
    INotificationService notificationService,
    IMapper mapper) : ControllerBase
{
    [HttpGet("GetAll")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserResponse>))]
    public async Task<ActionResult<IEnumerable<UserResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var users = await managementService.GetAllUsersAsync(cancellationToken);
        return Ok(mapper.Map<IEnumerable<UserResponse>>(users));
    }

    [HttpGet("GetUser/{id:guid}", Name = "GetUserById")]
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

    [HttpPost("RegisterUser")]
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

    [HttpPatch("ChangeUsername")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<ActionResult> ChangeUsername([FromBody] ChangeUsernameRequest request, CancellationToken cancellationToken)
    {
        var isAvailableUsername = await validationService.IsAvailableUsernameAsync(request.NewUsername, cancellationToken);
        if (!isAvailableUsername)
        {
            return BadRequest("Username is reserved.");
        }

        var userToUpdate = await managementService.GetUserByIdAsync(request.Id, cancellationToken);
        if (userToUpdate is null)
            return NotFound($"The user \"{request.Id}\" for the update does not exist");

        var changeUsernameModel = mapper.Map<ChangeUsernameModel>(request);
        var updateResult = await managementService.ChangeUsernameAsync(changeUsernameModel, cancellationToken);
        if (!updateResult)
            return NotFound();

        return NoContent();
    }

    [HttpPatch("ChangePasswordHash")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordRequest request, CancellationToken cancellationToken)
    {
        var validateModel = mapper.Map<ValidatePasswordModel>(request);
        var isOldPasswordValid = await validationService.ValidatePasswordAsync(validateModel, cancellationToken);
        if (!isOldPasswordValid)
        {
            return BadRequest("Old password not valid");
        }

        var userToUpdate = await managementService.GetUserByIdAsync(request.Id, cancellationToken);
        if (userToUpdate is null)
            return NotFound($"The user \"{request.Id}\" for the update does not exist");

        var changePasswordDto = mapper.Map<ChangePasswordModel>(request);

        var updateResult = await managementService.ChangePasswordAsync(changePasswordDto, cancellationToken);
        if (!updateResult)
            return NotFound();

        return NoContent();
    }

    [HttpPost("CreateEmailChangeRequest")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<ActionResult> CreateEmailChangeRequest([FromBody] ChangeEmailRequest request, CancellationToken cancellationToken)
    {
        var isAvailableEmail = await validationService.IsAvailableEmailAsync(request.NewEmail, cancellationToken);
        if (!isAvailableEmail)
        {
            return BadRequest("Email is reserved");
        }

        var userToUpdate = await managementService.GetUserByIdAsync(request.Id, cancellationToken);
        if (userToUpdate is null)
            return NotFound($"The user \"{request.Id}\" for the update does not exist");

        var changeEmailModel = mapper.Map<MailConfirmationGenerationModel>(request);

        var isCreated = await notificationService.CreateSetEmailRequest(changeEmailModel, cancellationToken);
        if (isCreated)
        {
            return Ok($"A request has been created to change the email address to {changeEmailModel.NewEmail}. Check your email for confirmation.");
        }

        return BadRequest();
    }

    [HttpPatch("VerifyEmail")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<ActionResult> VerifyEmail([FromBody] VerifyEmailRequest request, CancellationToken cancellationToken)
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

        var verifyEmailModel = mapper.Map<SetUserEmailModel>(request);

        var updateResult = await managementService.SetUserEmailAsync(verifyEmailModel, cancellationToken);

        if (!updateResult)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("DeleteUser/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<ActionResult<bool>> DeleteUser(Guid id, CancellationToken cancellationToken)
    {
        var userToDelete = await managementService.GetUserByIdAsync(id, cancellationToken);
        if (userToDelete is null)
            return NotFound($"The user \"{id}\" for the delete does not exist");

        var deleteResult = await managementService.SoftDeleteUserByIdAsync(id, cancellationToken);
        if (!deleteResult)
            return NotFound(deleteResult);

        return NoContent();
    }
}