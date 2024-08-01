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
    public async Task<ActionResult<IEnumerable<UserResponseModel>>> GetAll()
    {
        var users = await managementService.GetAllUsersAsync();
        return Ok(mapper.Map<IEnumerable<UserResponseModel>>(users));
    }
    [HttpGet]
    [Route("{id}", Name = "GetUserById")]
    public async Task<ActionResult<UserResponseModel>> GetUserById(Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest($"Id is empty");
        var user = await managementService.GetUserByIdAsync(id);
        if (user == null) //объясните
            return NotFound("The user with this id was not found");
        var userResponse = mapper.Map<UserResponseModel>(user);

        return Ok(userResponse);
    }

    [HttpPost]
    public async Task<ActionResult<UserResponseModel>> CreateUser(CreatingUserModel creatingUserModel)
    {
        if (creatingUserModel == null ||
            string.IsNullOrWhiteSpace(creatingUserModel.Username) ||
            string.IsNullOrWhiteSpace(creatingUserModel.Password) ||
            string.IsNullOrWhiteSpace(creatingUserModel.Email))
            return BadRequest("Empty input in required fields");// сделать валидацию ввода
        var createUserDto = mapper.Map<CreateUserDto>(creatingUserModel);
        var createdUser = await managementService.CreateUserAsync(createUserDto);
        var userResponse = mapper.Map<UserResponseModel>(createdUser);
        return CreatedAtAction(nameof(GetUserById), new { userResponse.Id }, userResponse);
    }

    [HttpPut]
    [Route("ChangeUsername/")]
    public async Task<ActionResult<UserResponseModel>> ChangeUsername([FromBody] ChangeUsernameModel changeUsernameModel)
    {
        if (changeUsernameModel == null ||
            changeUsernameModel.Id == Guid.Empty ||
            string.IsNullOrWhiteSpace(changeUsernameModel.Username))
            return BadRequest("Empty input in required fields");// сделать валидацию ввода

        var changeUsernameDto = mapper.Map<ChangeUsernameDto>(changeUsernameModel);

        var updatedUser = await managementService.ChangeUsernameAsync(changeUsernameDto);

        var userResponse = mapper.Map<UserResponseModel>(updatedUser);
        return CreatedAtAction(nameof(GetUserById), new { userResponse.Id }, userResponse);
    }

    [HttpPut]
    [Route("ChangePassword/")]
    public async Task<ActionResult<UserResponseModel>> ChangePassword([FromBody] ChangePasswordModel changePasswordModel)//атрибут from body?
    {
        if (changePasswordModel == null ||
            changePasswordModel.Id == Guid.Empty ||
            string.IsNullOrWhiteSpace(changePasswordModel.Password) ||
            string.IsNullOrWhiteSpace(changePasswordModel.NewPassword))
            return BadRequest("Empty input in required fields");// сделать валидацию ввода

        var changePasswordDto = mapper.Map<ChangePasswordDto>(changePasswordModel);

        var updatedUser = await managementService.ChangePasswordAsync(changePasswordDto);

        var userResponse = mapper.Map<UserResponseModel>(updatedUser);
        return CreatedAtAction(nameof(GetUserById), new { userResponse.Id }, userResponse);
    }

    [HttpPost]
    [Route("ChangeEmail/")]
    public async Task<ActionResult<bool>> ChangeEmail([FromBody] ChangeEmailModel changeEmailModel)
    {
        if (changeEmailModel == null ||
            changeEmailModel.Id == Guid.Empty ||
            string.IsNullOrWhiteSpace(changeEmailModel.NewEmail))
            return BadRequest("Empty input in required fields");// сделать валидацию ввода

        var changeEmailDto = mapper.Map<PublicationOfEmailConfirmationDto>(changeEmailModel);

        var isCreated = await managementService.CreateEmailChangeRequestAsync(changeEmailDto);
        if (isCreated)
        {
            return Ok($"A request has been created to change the email address to {changeEmailDto.NewEmail}. Check your email for confirmation.");
        }

        return BadRequest();
    }

    [HttpPut]
    [Route("VerifyEmail/")]
    public async Task<ActionResult<UserResponseModel>> VerifyEmail([FromBody]VerifyEmailModel verifyEmailModel)
    {
        if (verifyEmailModel == null ||
            verifyEmailModel.Id == Guid.Empty ||
            string.IsNullOrWhiteSpace(verifyEmailModel.NewEmail))
            return BadRequest("Empty input in required fields");// сделать валидацию ввода

        var time = DateTime.Compare(DateTime.Now, verifyEmailModel.CreatedDateTime);
        if (time > 15)
            return BadRequest("The link expired");

        var verifyEmailDto = mapper.Map<VerifyEmailDto>(verifyEmailModel);

        var updatedUser = await managementService.VerifyEmail(verifyEmailDto);

        var userResponse = mapper.Map<UserResponseModel>(updatedUser);
        return CreatedAtAction(nameof(GetUserById), new { userResponse.Id }, userResponse);
    }

}