using AutoMapper;
using Domain.Entities;
using Repositories.Abstractions;
using Services.Abstractions;
using Services.Contracts;

namespace Services.Implementations;
public class UserManagementService(
    IUserRepository repository,
    IMapper mapper,
    IPasswordHasher hasher,
    IMessageBusPublisher publisher) : IUserManagementService
{
    public async Task<IEnumerable<UserReadDto>> GetAllUsersAsync()
    {
        var users = await repository.GetAllAsync();
        return mapper.Map<IEnumerable<UserReadDto>>(users);
    }
    public async Task<UserReadDto> GetUserByIdAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentNullException(nameof(id));
        var user = await repository.GetByIdAsync(id);
        return mapper.Map<UserReadDto>(user);
    }

    public async Task<UserReadDto> CreateUserAsync(CreateUserDto createUserDto)
    {
        if (createUserDto == null ||
            string.IsNullOrWhiteSpace(createUserDto.Username) ||
            string.IsNullOrWhiteSpace(createUserDto.Password) ||
            string.IsNullOrWhiteSpace(createUserDto.Email))
            throw new ArgumentNullException(nameof(createUserDto));

        var isAvailableEmail = await repository.CheckIsAvailableEmailAsync(createUserDto.Email);
        if (!isAvailableEmail)
            throw new Exception("The email is already taken");
        var isAvailableUsername = await repository.CheckIsAvailableUsernameAsync(createUserDto.Username);
        if (!isAvailableUsername)
            throw new Exception("The username is already taken");

        var guest = mapper.Map<Guest>(createUserDto);

        var user = guest.SignUp();
        var createdUser = await repository.AddAsync(user);
        if (createdUser == null)
            throw new Exception("the repository was unable to create an entity");// сделать кастомный эксепшн

        var publishCreatedUser = mapper.Map<PublicationOfEmailConfirmationDto>(createdUser);
        await publisher.PublishNewEmail(publishCreatedUser);
        
        return mapper.Map<UserReadDto>(user);
    }

    public async Task<UserReadDto> ChangeUsernameAsync(ChangeUsernameDto changeUsernameDto)
    {
        if (changeUsernameDto == null ||
            changeUsernameDto.Id == Guid.Empty ||
            string.IsNullOrWhiteSpace(changeUsernameDto.Username))
            throw new ArgumentNullException(nameof(changeUsernameDto));

        var isAvailableUsername = await repository.CheckIsAvailableUsernameAsync(changeUsernameDto.Username);
        if (!isAvailableUsername)
            throw new Exception("The username is already taken");

        var user = await repository.GetByIdAsync(changeUsernameDto.Id);
        if (user == null)
            throw new Exception("The entity to change was not found in the repository");

        user.ChangeUsername(changeUsernameDto.Username);

        var updatedUser = await repository.UpdateAsync(changeUsernameDto.Id, user);
        return mapper.Map<UserReadDto>(updatedUser);
    }

    public async Task<UserReadDto> ChangePasswordAsync(ChangePasswordDto changePasswordDto)
    {
        if (changePasswordDto == null ||
            changePasswordDto.Id == Guid.Empty ||
            string.IsNullOrWhiteSpace(changePasswordDto.Password) ||
            string.IsNullOrWhiteSpace(changePasswordDto.NewPassword))
            throw new ArgumentNullException(nameof(changePasswordDto));

        var user = await repository.GetByIdAsync(changePasswordDto.Id);
        if (user == null)
            throw new Exception("The entity to change was not found in the repository");

        var isValidPassword = hasher.VerifyHashedPassword(changePasswordDto.Password, user.PasswordHash.Value);
        if (!isValidPassword)
            throw new Exception("Invalid password");

        var newPassword = hasher.GenerateHashPassword(changePasswordDto.NewPassword);
        user.ChangePassword(newPassword);

        var updatedUser = await repository.UpdateAsync(changePasswordDto.Id, user);
        return mapper.Map<UserReadDto>(updatedUser);
    }

    public async Task<bool> CreateEmailChangeRequestAsync(PublicationOfEmailConfirmationDto publicationOfEmailConfirmationDto)
    {
        if (publicationOfEmailConfirmationDto == null ||
            publicationOfEmailConfirmationDto.Id == Guid.Empty ||
            string.IsNullOrWhiteSpace(publicationOfEmailConfirmationDto.NewEmail))
            throw new ArgumentNullException(nameof(publicationOfEmailConfirmationDto));

        var isAvailableEmail = await repository.CheckIsAvailableEmailAsync(publicationOfEmailConfirmationDto.NewEmail);
        if (!isAvailableEmail)
            throw new Exception("The email is already taken");

        var user = await repository.GetByIdAsync(publicationOfEmailConfirmationDto.Id);
        if (user == null)
            throw new Exception("The entity to change was not found in the repository");

        await publisher.PublishNewEmail(publicationOfEmailConfirmationDto);

        return true;
    }

    public async Task<UserReadDto> VerifyEmail(VerifyEmailDto verifyEmailDto)
    {
        if (verifyEmailDto == null ||
            verifyEmailDto.Id == Guid.Empty ||
            string.IsNullOrWhiteSpace(verifyEmailDto.NewEmail))
            throw new ArgumentNullException(nameof(verifyEmailDto));

        var isAvailableEmail = await repository.CheckIsAvailableEmailAsync(verifyEmailDto.NewEmail);
        if (!isAvailableEmail)
            throw new Exception("The email is already taken");

        var user = await repository.GetByIdAsync(verifyEmailDto.Id);
        if (user == null)
            throw new Exception("The entity to change was not found in the repository");

        user.ConfirmNewEmail(verifyEmailDto.NewEmail);

        var updatedUser = await repository.UpdateAsync(verifyEmailDto.Id, user);
        return mapper.Map<UserReadDto>(updatedUser);
    }
}