using Services.Contracts;

namespace Services.Abstractions;

public interface IUserManagementService
{
    public Task<UserReadDto> GetUserByIdAsync(Guid id);
    public Task<UserReadDto> CreateUserAsync(CreateUserDto createUserDto);
    public Task<bool> ChangePasswordAsync(ChangePasswordDto changePasswordDto);
    public Task<bool> ChangeEmailAsync(ChangeEmailDto changeEmailDto);
    public Task<bool> VerifyEmailAsync(VerifyEmailDto verifyEmailDto);
    public Task<bool> RestrictUserAsync(RestrictUserDto restrictUserDto);
}