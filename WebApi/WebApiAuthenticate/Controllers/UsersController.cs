using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Contracts;
using WebApiAuthenticate.Models;

namespace WebApiAuthenticate.Controllers;

//поднять вопрос о разделении контроллера на несколько
[ApiController]
[Route("/api/v1/[controller]")]
public class UsersController(
    IUserManagementService managementService,
    IMapper mapper) : ControllerBase
{

    [HttpGet("GetAll/")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserReadResponse>))]
    public async Task<ActionResult<IEnumerable<UserReadResponse>>> GetAll()
    {
        var users = await managementService.GetAllUsersAsync();
        return Ok(mapper.Map<IEnumerable<UserReadResponse>>(users));
    }

    [HttpGet("GetUser/{id:guid}", Name = "GetUserById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserReadResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<ActionResult<UserReadResponse>> GetUserById(Guid id)
    {
        var user = await managementService.GetUserByIdAsync(id);
        if (user == null)
            return NotFound($"The user with this id - \"{id}\" was not found");

        var userResponse = mapper.Map<UserReadResponse>(user);
        return Ok(userResponse);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserReadResponse))]
    //При валидации используется ли возврат 400? надо ли делать атрибут?
    public async Task<ActionResult<UserReadResponse>> CreateUser([FromBody] CreatingUserRequest creatingUserRequest)
    {
        var createUserDto = mapper.Map<CreateUserModel>(creatingUserRequest);
        var createdUser = await managementService.CreateUserAsync(createUserDto);

        var userResponse = mapper.Map<UserReadResponse>(createdUser);
        return CreatedAtAction(nameof(GetUserById), new { userResponse.Id }, userResponse);
    }

    [HttpPut("ChangeUsername/")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserReadResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<ActionResult<UserReadResponse>> ChangeUsername([FromBody] ChangeUsernameRequest changeUsernameRequest)
    {
        var userToUpdate = await managementService.GetUserByIdAsync(changeUsernameRequest.Id);
        if (userToUpdate is null)
            return NotFound($"The user \"{changeUsernameRequest.Id}\" for the update does not exist");

        var changeUsernameModel = mapper.Map<ChangeUsernameModel>(changeUsernameRequest);
        var updateResult = await managementService.ChangeUsernameAsync(changeUsernameModel);

        var userResponse = mapper.Map<UserReadResponse>(updateResult);
        return CreatedAtAction(nameof(GetUserById), new { userResponse.Id }, userResponse);
    }

    [HttpPut("ChangePassword/")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserReadResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<ActionResult<UserReadResponse>> ChangePassword([FromBody] ChangePasswordRequest changePasswordRequest)
    {
        var userToUpdate = await managementService.GetUserByIdAsync(changePasswordRequest.Id);
        if (userToUpdate is null)
            return NotFound($"The user \"{changePasswordRequest.Id}\" for the update does not exist");

        var validatePasswordModel = mapper.Map<ValidatePasswordModel>(changePasswordRequest);
        var isValidPassword = await managementService.ValidatePassword(validatePasswordModel);
        if (!isValidPassword)
            return BadRequest($"Invalid password - {validatePasswordModel.Password}");

        var changePasswordDto = mapper.Map<ChangePasswordModel>(changePasswordRequest);

        var updatedUser = await managementService.ChangePasswordAsync(changePasswordDto);

        var userResponse = mapper.Map<UserReadResponse>(updatedUser);
        return CreatedAtAction(nameof(GetUserById), new { userResponse.Id }, userResponse);
    }

    [HttpPost("ChangeEmail/")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<ActionResult> ChangeEmail([FromBody] ChangeEmailRequest changeEmailRequest)
    {
        var userToUpdate = await managementService.GetUserByIdAsync(changeEmailRequest.Id);
        if (userToUpdate is null)
            return NotFound($"The user \"{changeEmailRequest.Id}\" for the update does not exist");

        var changeEmailModel = mapper.Map<PublicationOfEmailConfirmationModel>(changeEmailRequest);

        var isCreated = await managementService.CreateEmailChangeRequestAsync(changeEmailModel);
        if (isCreated)
        {
            return Ok($"A request has been created to change the email address to {changeEmailModel.NewEmail}. Check your email for confirmation.");
        }

        return BadRequest(); //что вернуть? Если не создалось в целом исключительная ситуация?
    }

    [HttpPut("VerifyEmail/")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserReadResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<ActionResult<UserReadResponse>> VerifyEmail([FromBody] VerifyEmailRequest verifyEmailRequest)
    {
        var userToUpdate = await managementService.GetUserByIdAsync(verifyEmailRequest.Id);
        if (userToUpdate is null)
            return NotFound($"The user \"{verifyEmailRequest.Id}\" for the update does not exist");

        if (userToUpdate.Email != verifyEmailRequest.NewEmail)
        {
            var isAvailableEmail = await managementService.CheckAvailableEmailAsync(verifyEmailRequest.NewEmail);
            if (!isAvailableEmail)
                return BadRequest($"Email - {verifyEmailRequest.NewEmail} is reserved.");
        }

        var time = DateTime.Now - verifyEmailRequest.CreatedDateTime;
        if (time.TotalMinutes > 15)
            return BadRequest("The link expired");

        var verifyEmailModel = mapper.Map<VerifyEmailModel>(verifyEmailRequest);

        var updatedUser = await managementService.VerifyEmail(verifyEmailModel);

        var userResponse = mapper.Map<UserReadResponse>(updatedUser);
        return CreatedAtAction(nameof(GetUserById), new { userResponse.Id }, userResponse);
    }

    [HttpDelete("DeleteUser/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<ActionResult<bool>> DeleteUser(Guid id)
    {
        var userToDelete = await managementService.GetUserByIdAsync(id);
        if (userToDelete is null)
            return NotFound($"The user \"{id}\" for the delete does not exist");

        var deleteResult = await managementService.DeleteUserById(id);
        if (!deleteResult)
        {
            return NotFound(deleteResult);
        }

        return NoContent();
    }
}