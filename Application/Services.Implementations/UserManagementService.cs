using Domain.Entities;
using Domain.ValueObjects.ValueObjects;
using Repositories.Abstractions;
using Services.Abstractions;
using Services.Contracts;

namespace Services.Implementations;
public class UserManagementService(
    IUserRepository repository,
    IHasher passwordHasher,
    IMapper mapper,
    INotificationPublisher notificationPublisher) : IUserManagementService
{
    public async Task<UserReadDto> GetUserByIdAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentNullException(nameof(id));

        var user = await repository.GetByIdAsync(id);
        return mapper.Map<UserReadDto>(user);
    }
    public async Task<bool> CreateUserAsync(CreateUserDto createUserDto)
    {
        if ((createUserDto == null)||
            createUserDto.Id == Guid.Empty ||
            string.IsNullOrWhiteSpace(createUserDto.Username) ||
            string.IsNullOrWhiteSpace(createUserDto.Password) ||
            string.IsNullOrWhiteSpace(createUserDto.Email))
            throw new ArgumentNullException(nameof(createUserDto));

        var guest = mapper.Map<Guest>(createUserDto);

        var isAvailableUsername = await repository.CheckIsAvailableUsername(guest.Username.Value);
        if (!isAvailableUsername)
            return false;
        

        var isAvailableEmail = await repository.CheckIsAvailableEmail(guest.Email.Value);
        if (!isAvailableEmail)
            return false;

        var user = guest.SignUp();
        await repository.AddAsync(user);

        var publishVerifyEmailDto = mapper.Map<VerifyEmailDto>(user);
        await notificationPublisher.PublishVerifyEmailLinkAsync(publishVerifyEmailDto);

        return true;
    }
    public async Task<bool> ChangeUsername(ChangeUsernameDto changeUsernameDto)
    {
        if ((changeUsernameDto == null) ||
            (changeUsernameDto.UserId == Guid.Empty) ||
            (string.IsNullOrWhiteSpace(changeUsernameDto.NewUsername)))
            throw new ArgumentNullException(nameof(changeUsernameDto));

        var isAvailableUsername = await repository.CheckIsAvailableUsername(changeUsernameDto.NewUsername);
        if (!isAvailableUsername)
            return false;

        var user = await repository.GetByIdAsync(changeUsernameDto.UserId);
        if (user == null)
            return false;


        user.ChangeUsername(new Username(changeUsernameDto.NewUsername));
        return await repository.UpdateUserAsync(changeUsernameDto.UserId, user);
    }
    public async Task<bool> ChangePasswordAsync(ChangePasswordDto changePasswordDto)
    {
        if ((changePasswordDto == null)||
            changePasswordDto.UserId == Guid.Empty ||
            string.IsNullOrWhiteSpace(changePasswordDto.PasswordToChange) ||
            string.IsNullOrWhiteSpace(changePasswordDto.PasswordVerification))
            throw new ArgumentNullException(nameof(changePasswordDto));

        
        var user = await repository.GetByIdAsync(changePasswordDto.UserId);
        if (user == null)
            return false;

        if (!passwordHasher.VerifyPassword(changePasswordDto.PasswordVerification, user.PasswordHash.Value))
            return false;
        var passwordHashToChange = passwordHasher.GeneratePassword(changePasswordDto.PasswordToChange);

        user.ChangePassword(new PasswordHash(passwordHashToChange));
        return await repository.UpdateUserAsync(changePasswordDto.UserId, user);
    }
    public async Task<bool> ChangeEmailAsync(ChangeEmailDto changeEmailDto)
    {
        if ((changeEmailDto == null) ||
            (changeEmailDto.Id == Guid.Empty)||
            (string.IsNullOrWhiteSpace(changeEmailDto.NewEmail)))
            throw new ArgumentNullException(nameof(changeEmailDto));

        var user = await repository.GetByIdAsync(changeEmailDto.Id);
        if (user == null)
            return false;

        var isAvailableEmail = await repository.CheckIsAvailableEmail(changeEmailDto.NewEmail);
        if (!isAvailableEmail)
            return false;

        var publishVerifyEmailDto = mapper.Map<PublishVerifyEmailDto>(changeEmailDto);
        await notificationPublisher.PublishVerifyEmailLinkAsync(publishVerifyEmailDto);

        return true;
    }
    public async Task<bool> VerifyEmailAsync(VerifyEmailDto? verifyEmailDto)
    {
        if ((verifyEmailDto == null)||
            (verifyEmailDto.UserId == Guid.Empty)||
            (string.IsNullOrWhiteSpace(verifyEmailDto.VerifiedEmail)))
            throw new ArgumentNullException(nameof(verifyEmailDto));
        
        var newEmail = new Email(verifyEmailDto.VerifiedEmail);

        var user = await repository.GetByIdAsync(verifyEmailDto.UserId);
        if (user == null)
            return false;

        var isAvailableEmail = await repository.CheckIsAvailableEmail(newEmail.Value);
        if (isAvailableEmail)
            return false; //рассмотреть возможность кастомного Exception

        user.ConfirmNewEmail(newEmail);
        return await repository.UpdateUserAsync(verifyEmailDto.UserId, user);
    }
    public async Task<bool> RestrictUserAsync(RestrictUserDto restrictUserDto)
    {
        if ((restrictUserDto == null)||
            (restrictUserDto.Id == Guid.Empty))
            throw new ArgumentNullException(nameof(restrictUserDto));

        var user = await repository.GetByIdAsync(restrictUserDto.Id);
        if (user == null)
            return false;
        user.RestrictUser();

        return await repository.UpdateUserAsync(restrictUserDto.Id, user);
    }
}