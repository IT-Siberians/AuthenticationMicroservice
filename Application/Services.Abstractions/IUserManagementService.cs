using Services.Contracts;

namespace Services.Abstractions;

public interface IUserManagementService
{
    public Task<UserReadDto> GetUserByIdAsync(Guid id);
    public Task<bool> CreateUserAsync(CreateUserDto createUserDto);
    public Task<bool> ChangeUsername(ChangeUsernameDto changeUsernameDto);
    public Task<bool> ChangePasswordAsync(ChangePasswordDto changePasswordDto);
    public Task<bool> ChangeEmailAsync(ChangeEmailDto changeEmailDto);
    public Task<bool> VerifyEmailAsync(VerifyEmailDto? verifyEmailDto);
    public Task<bool> RestrictUserAsync(RestrictUserDto restrictUserDto);
}