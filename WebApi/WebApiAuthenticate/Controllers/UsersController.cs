using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Contracts;
using WebApiAuthenticate.Requests;
using WebApiAuthenticate.Responses;
using static WebApiAuthenticate.Helpers.AttributePoliticsNameHelpers;

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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<UserInfoResponse>>))]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var users = await managementService.GetAllUsersAsync(cancellationToken);
        var usersInfos = mapper.Map<IEnumerable<UserInfoResponse>>(users);
        return Ok(new ApiResponse<IEnumerable<UserInfoResponse>>(usersInfos));
    }

    [HttpGet("{id:guid}", Name = "GetUserById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<UserInfoResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse<string>))]
    public async Task<IActionResult> GetUserById(Guid id, CancellationToken cancellationToken)
    {
        var user = await managementService.GetUserByIdAsync(id, cancellationToken);
        if (user == null)
            return NotFound(new ApiResponse<string>($"The user with this id - \"{id}\" was not found"));

        var userResponse = mapper.Map<UserInfoResponse>(user);
        return Ok(new ApiResponse<UserInfoResponse>(userResponse));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ApiResponse<UserInfoResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<string>))]
    public async Task<IActionResult> CreateUser([FromBody] CreatingUserRequest request, CancellationToken cancellationToken)
    {
        var isAvailableUsername = await validationService.IsAvailableUsernameAsync(request.Username, cancellationToken);
        if (!isAvailableUsername)
            return BadRequest(new ApiResponse<string>("Username is reserved."));

        var isAvailableEmail = await validationService.IsAvailableEmailAsync(request.Email, cancellationToken);
        if (!isAvailableEmail)
            return BadRequest(new ApiResponse<string>("Email is reserved"));

        var createUserDto = mapper.Map<CreateUserModel>(request);
        var createdUser = await managementService.CreateUserAsync(createUserDto, cancellationToken);

        var userResponse = mapper.Map<UserInfoResponse>(createdUser);
        return CreatedAtAction(nameof(GetUserById), new { userResponse.Id }, userResponse);
    }

    [Authorize(Policy = OWNER_ONLY_POLITIC_NAME)]
    [HttpPatch("{id:guid}/ChangePassword")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<string>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse<string>))]
    public async Task<IActionResult> ChangePassword(Guid id, [FromBody] ChangePasswordRequest request, CancellationToken cancellationToken)
    {
        var validateModel = new ValidatePasswordModel(id, request.OldPassword);
        var isOldPasswordValid = await validationService.ValidatePasswordAsync(validateModel, cancellationToken);
        if (!isOldPasswordValid)
            return BadRequest(new ApiResponse<string>("Old password not valid"));

        var userToUpdate = await managementService.GetUserByIdAsync(id, cancellationToken);
        if (userToUpdate is null)
            return NotFound(new ApiResponse<string>($"The user \"{id}\" for the update does not exist"));

        var changePasswordModel = new ChangePasswordModel(id, request.NewPassword);

        var updateResult = await managementService.ChangePasswordAsync(changePasswordModel, cancellationToken);
        if (!updateResult)
            return NotFound(new ApiResponse<string>("Updated user is null"));

        return NoContent();
    }

    [Authorize(Policy = OWNER_ONLY_POLITIC_NAME)]
    [HttpPost("{id:guid}/ChangeEmail")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<string>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse<string>))]
    public async Task<IActionResult> CreateEmailChangeRequest(Guid id, [FromBody] NewEmailRequest request, CancellationToken cancellationToken)
    {
        var userToUpdate = await managementService.GetUserByIdAsync(id, cancellationToken);
        if (userToUpdate is null)
            return NotFound(new ApiResponse<string>($"The user \"{id}\" for the update does not exist"));

        if (userToUpdate.Email != request.EmailValue)
        {
            var isAvailableEmail = await validationService.IsAvailableEmailAsync(request.EmailValue, cancellationToken);
            if (!isAvailableEmail)
                return BadRequest(new ApiResponse<string>("Email is reserved"));
        }

        var changeEmailModel = new EmailConfirmationModel(id, request.EmailValue);

        var isCreated = await notificationService.SendEmailConfirmationAsync(changeEmailModel, cancellationToken);
        if (isCreated)
            return NoContent();

        return BadRequest(new ApiResponse<string>("Unknown error"));
    }

    [Authorize(Policy = OWNER_ONLY_POLITIC_NAME)]
    [HttpDelete("{id:guid}/Delete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse<bool>))]
    public async Task<ActionResult<bool>> DeleteUser(Guid id, CancellationToken cancellationToken)
    {
        var userToDelete = await managementService.GetUserByIdAsync(id, cancellationToken);
        if (userToDelete is null)
            return NotFound(new ApiResponse<string>($"The user \"{id}\" for the delete does not exist"));

        var deleteResult = await managementService.DeleteUserSoftlyByIdAsync(id, cancellationToken);
        if (!deleteResult)
            return NotFound(new ApiResponse<bool>(deleteResult));

        return NoContent();
    }
}