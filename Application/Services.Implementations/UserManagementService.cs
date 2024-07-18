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
        {
            throw new ArgumentNullException(nameof(id));
        }

        var user = await repository.GetByIdAsync(id);
        return mapper.Map<UserReadDto>(user);
    }

    public async Task<UserReadDto> CreateUserAsync(CreateUserDto createUserDto)
    {
        if (createUserDto == null)
        {
            throw new ArgumentNullException(nameof(createUserDto));
        }

        Guest guest = mapper.Map<Guest>(createUserDto);

        var user = guest.SignUp();
        await repository.AddAsync(user);

        var publishVerifyEmailDto = mapper.Map<VerifyEmailDto>(user);
        await notificationPublisher.PublishVerifyEmailLinkAsync(publishVerifyEmailDto);

        return mapper.Map<UserReadDto>(user);
    }

    public async Task<bool> ChangePasswordAsync(ChangePasswordDto changePasswordDto)
    {
        if (changePasswordDto == null)
        {
            throw new ArgumentNullException(nameof(changePasswordDto));
        }

        var userId = changePasswordDto.UserId;
        if (userId == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(userId));
        }

        var user = await repository.GetByIdAsync(userId);
        if (user == null)
            return false;//что лучше фолс или эксепшн?

        if (!passwordHasher.VerifyPassword(changePasswordDto.PasswordVerification, user.PasswordHash.Value))
            return false;
        var passwordHashToChange = passwordHasher.GeneratePassword(changePasswordDto.PasswordToChange);
        user.ChangePassword(new PasswordHash(passwordHashToChange));
        await repository.UpdateUserAsync(userId, user);
        return true;
    }

    public async Task<bool> ChangeEmailAsync(ChangeEmailDto changeEmailDto)
    {
        if (changeEmailDto == null)
        {
            throw new ArgumentNullException(nameof(changeEmailDto));
        }

        var userId = changeEmailDto.Id;
        if (userId == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(userId));
        }

        var user = await repository.GetByIdAsync(userId);
        if (user == null)
            return false;

        var publishVerifyEmailDto = mapper.Map<PublishVerifyEmailDto>(changeEmailDto); // валидация эмейл через маппер(это точно возмонжо)
        await notificationPublisher.PublishVerifyEmailLinkAsync(publishVerifyEmailDto); // кто собирает ссылку?

        return true;
    }

    public async Task<bool> VerifyEmailAsync(VerifyEmailDto verifyEmailDto)
    {
        if (verifyEmailDto == null)
        {
            throw new ArgumentNullException(nameof(verifyEmailDto));
        }

        var userId = verifyEmailDto.UserId;
        if (userId == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(verifyEmailDto.UserId));
        }

        var emailString = verifyEmailDto.VerifiedEmail;
        var newEmail = new Email(emailString);

        var user = await repository.GetByIdAsync(userId);
        if (user == null)
            return false;//что лучше фолс или эксепшн?
        user.ConfirmNewEmail(newEmail);
        return await repository.UpdateUserAsync(userId, user);

    }

    public async Task<bool> RestrictUserAsync(RestrictUserDto restrictUserDto)
    {
        if (restrictUserDto == null)
        {
            throw new ArgumentNullException(nameof(restrictUserDto));
        }

        var restrictedId = restrictUserDto.Id;
        if (restrictedId == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(restrictedId));
        }

        var user = await repository.GetByIdAsync(restrictedId);
        if (user == null)
            return false;//что лучше фолс или эксепшн?
        user.RestrictUser();

        return await repository.UpdateUserAsync(restrictedId, user);
    }
}