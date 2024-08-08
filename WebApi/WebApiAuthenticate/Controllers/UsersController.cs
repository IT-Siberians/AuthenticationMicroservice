using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Contracts;
using WebApiAuthenticate.Models;

namespace WebApiAuthenticate.Controllers;
[ApiController]
[Route("/api/[controller]")]
public class UsersController(
    IUserManagementService managementService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserReadResponse>>> GetAll()
    {
        var users = await managementService.GetAllUsersAsync();
        return Ok(mapper.Map<IEnumerable<UserReadResponse>>(users));
    }
    [HttpGet]
    [Route("GetUser/{id}", Name = "GetUserById")]
    public async Task<ActionResult<UserReadResponse>> GetUserById(Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest($"Id is empty");
        var user = await managementService.GetUserByIdAsync(id);
        if (user == null) //объясните
            return NotFound("The user with this id was not found");
        var userResponse = mapper.Map<UserReadResponse>(user);

        return Ok(userResponse);
    }

    [HttpPost]
    public async Task<ActionResult<UserReadResponse>> CreateUser(CreatingUserRequest creatingUserRequest)
    {
        if (creatingUserRequest == null ||
            string.IsNullOrWhiteSpace(creatingUserRequest.Username) ||
            string.IsNullOrWhiteSpace(creatingUserRequest.Password) ||
            string.IsNullOrWhiteSpace(creatingUserRequest.Email))
            return BadRequest("Empty input in required fields");// сделать валидацию ввода
        var createUserDto = mapper.Map<CreateUserModel>(creatingUserRequest);
        var createdUser = await managementService.CreateUserAsync(createUserDto);
        var userResponse = mapper.Map<UserReadResponse>(createdUser);
        return CreatedAtAction(nameof(GetUserById), new { userResponse.Id }, userResponse);
    }

    [HttpPut]
    [Route("ChangeUsername/")]
    public async Task<ActionResult<UserReadResponse>> ChangeUsername([FromBody] ChangeUsernameRequest changeUsernameRequest)
    {
        if (changeUsernameRequest == null ||
            changeUsernameRequest.Id == Guid.Empty ||
            string.IsNullOrWhiteSpace(changeUsernameRequest.Username))
            return BadRequest("Empty input in required fields");// сделать валидацию ввода

        var changeUsernameDto = mapper.Map<ChangeUsernameModel>(changeUsernameRequest);

        var updatedUser = await managementService.ChangeUsernameAsync(changeUsernameDto);

        var userResponse = mapper.Map<UserReadResponse>(updatedUser);
        return CreatedAtAction(nameof(GetUserById), new { userResponse.Id }, userResponse);
    }

    [HttpPut]
    [Route("ChangePassword/")]
    public async Task<ActionResult<UserReadResponse>> ChangePassword([FromBody] ChangePasswordRequest changePasswordRequest)//атрибут from body?
    {
        if (changePasswordRequest == null ||
            changePasswordRequest.Id == Guid.Empty ||
            string.IsNullOrWhiteSpace(changePasswordRequest.Password) ||
            string.IsNullOrWhiteSpace(changePasswordRequest.NewPassword))
            return BadRequest("Empty input in required fields");// сделать валидацию ввода

        var changePasswordDto = mapper.Map<ChangePasswordModel>(changePasswordRequest);

        var updatedUser = await managementService.ChangePasswordAsync(changePasswordDto);

        var userResponse = mapper.Map<UserReadResponse>(updatedUser);
        return CreatedAtAction(nameof(GetUserById), new { userResponse.Id }, userResponse);
    }

    [HttpPost]
    [Route("ChangeEmail/")]
    public async Task<ActionResult<bool>> ChangeEmail([FromBody] ChangeEmailRequest changeEmailRequest)
    {
        if (changeEmailRequest == null ||
            changeEmailRequest.Id == Guid.Empty ||
            string.IsNullOrWhiteSpace(changeEmailRequest.NewEmail))
            return BadRequest("Empty input in required fields");// сделать валидацию ввода

        var changeEmailDto = mapper.Map<PublicationOfEmailConfirmationModel>(changeEmailRequest);

        var isCreated = await managementService.CreateEmailChangeRequestAsync(changeEmailDto);
        if (isCreated)
        {
            return Ok($"A request has been created to change the email address to {changeEmailDto.NewEmail}. Check your email for confirmation.");
        }

        return BadRequest();
    }

    [HttpPut]
    [Route("VerifyEmail/")]
    public async Task<ActionResult<UserReadResponse>> VerifyEmail([FromBody]VerifyEmailRequest verifyEmailRequest)
    {
        if (verifyEmailRequest == null ||
            verifyEmailRequest.Id == Guid.Empty ||
            string.IsNullOrWhiteSpace(verifyEmailRequest.NewEmail))
            return BadRequest("Empty input in required fields");// сделать валидацию ввода

        var time = DateTime.Now - verifyEmailRequest.CreatedDateTime;
        if (time.TotalMinutes > 15)
            return BadRequest("The link expired");

        var verifyEmailDto = mapper.Map<VerifyEmailModel>(verifyEmailRequest);

        var updatedUser = await managementService.VerifyEmail(verifyEmailDto);

        var userResponse = mapper.Map<UserReadResponse>(updatedUser);
        return CreatedAtAction(nameof(GetUserById), new { userResponse.Id }, userResponse);
    }

    [HttpDelete]
    [Route("DeleteUser/{id}")]
    public async Task<ActionResult<bool>> DeleteUser(Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest($"Id is empty");
        var deleteResult = await managementService.DeleteUserById(id);
        if (deleteResult)
        {
            return Ok(deleteResult);
        }
        return NotFound(deleteResult);
    }

}