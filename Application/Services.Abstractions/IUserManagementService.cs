using Services.Contracts;

namespace Services.Abstractions;
public interface IUserManagementService
{
    public Task<IEnumerable<UserReadDto>> GetAllUsersAsync();
    public Task<UserReadDto> GetUserByIdAsync(Guid id);

    Task<UserReadDto> CreateUserAsync(CreateUserDto createUserDto);
    Task<UserReadDto> ChangeUsernameAsync(ChangeUsernameDto changeUsernameDto);
    Task<UserReadDto> ChangePasswordAsync(ChangePasswordDto changePasswordDto);
    Task<bool> CreateEmailChangeRequestAsync(PublicationOfEmailConfirmationDto publicationOfEmailConfirmationDto);
    Task<UserReadDto> VerifyEmail(VerifyEmailDto verifyEmailDto);
}