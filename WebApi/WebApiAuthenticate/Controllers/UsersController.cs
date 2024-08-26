using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Contracts;
using WebApiAuthenticate.Requests;
using WebApiAuthenticate.Responses;

namespace WebApiAuthenticate.Controllers;

//поднять вопрос о разделении контроллера на несколько, 25.08 вопрос остается открытый нужна помощь
[ApiController]
[Route("/api/v1/[controller]")]
public class UsersController(
    IUserManagementService managementService,
    INotificationService notificationService,
    IMapper mapper) : ControllerBase
{
    [HttpGet("GetAll")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserReadResponse>))]
    public async Task<ActionResult<IEnumerable<UserReadResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var users = await managementService.GetAllUsersAsync(cancellationToken);
        return Ok(mapper.Map<IEnumerable<UserReadResponse>>(users));
    }

    [HttpGet("GetUser/{id:guid}", Name = "GetUserById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserReadResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<ActionResult<UserReadResponse>> GetUserById(Guid id, CancellationToken cancellationToken)
    {
        var user = await managementService.GetUserByIdAsync(id, cancellationToken);
        if (user == null)
            return NotFound($"The user with this id - \"{id}\" was not found");

        var userResponse = mapper.Map<UserReadResponse>(user);
        return Ok(userResponse);
    }

    [HttpPost("RegisterUser")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserReadResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    public async Task<ActionResult<UserReadResponse>> CreateUser([FromBody] CreatingUserRequest creatingUserRequest, CancellationToken cancellationToken)
    {
        var createUserDto = mapper.Map<CreateUserModel>(creatingUserRequest);
        var createdUser = await managementService.CreateUserAsync(createUserDto, cancellationToken);
        if (createdUser == null)
            return BadRequest("The user has not been created");

        var userResponse = mapper.Map<UserReadResponse>(createdUser);
        return CreatedAtAction(nameof(GetUserById), new { userResponse.Id }, userResponse);
    }

    [HttpPatch("ChangeUsername")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<ActionResult> ChangeUsername([FromBody] ChangeUsernameRequest changeUsernameRequest, CancellationToken cancellationToken)
    {
        var userToUpdate = await managementService.GetUserByIdAsync(changeUsernameRequest.Id, cancellationToken);
        if (userToUpdate is null)
            return NotFound($"The user \"{changeUsernameRequest.Id}\" for the update does not exist");

        var changeUsernameModel = mapper.Map<ChangeUsernameModel>(changeUsernameRequest);
        var updateResult = await managementService.ChangeUsernameAsync(changeUsernameModel, cancellationToken);
        if (!updateResult)
            return NotFound();

        return NoContent();
    }

    [HttpPatch("ChangePasswordHash")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordRequest changePasswordRequest, CancellationToken cancellationToken)
    {
        var userToUpdate = await managementService.GetUserByIdAsync(changePasswordRequest.Id, cancellationToken);
        if (userToUpdate is null)
            return NotFound($"The user \"{changePasswordRequest.Id}\" for the update does not exist");

        var changePasswordDto = mapper.Map<ChangePasswordModel>(changePasswordRequest);

        var updateResult = await managementService.ChangePasswordAsync(changePasswordDto, cancellationToken);
        if (!updateResult)
            return NotFound();

        return NoContent();
    }

    [HttpPost("CreateEmailChangeRequest")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<ActionResult> CreateEmailChangeRequest([FromBody] ChangeEmailRequest changeEmailRequest, CancellationToken cancellationToken)
    {
        var userToUpdate = await managementService.GetUserByIdAsync(changeEmailRequest.Id, cancellationToken);
        if (userToUpdate is null)
            return NotFound($"The user \"{changeEmailRequest.Id}\" for the update does not exist");

        var changeEmailModel = mapper.Map<MailConfirmationGenerationModel>(changeEmailRequest);

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
    public async Task<ActionResult> VerifyEmail([FromBody] VerifyEmailRequest verifyEmailRequest, CancellationToken cancellationToken)
    {
        var userToUpdate = await managementService.GetUserByIdAsync(verifyEmailRequest.Id, cancellationToken);
        if (userToUpdate is null)
            return NotFound($"The user \"{verifyEmailRequest.Id}\" for the update does not exist");

        var verifyEmailModel = mapper.Map<SetUserEmailModel>(verifyEmailRequest);

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