using Services.Contracts;

namespace Services.Abstractions;

public interface IUserManagementService
{
    public Task<IEnumerable<UserReadModel>> GetAllUsersAsync();
    public Task<UserReadModel> GetUserByIdAsync(Guid id);

    public Task<UserReadModel> CreateUserAsync(CreateUserModel createUserModel);
    public Task<UserReadModel> ChangeUsernameAsync(ChangeUsernameModel changeUsernameModel);
    public Task<UserReadModel> ChangePasswordAsync(ChangePasswordModel changePasswordModel);
    public Task<bool> CreateEmailChangeRequestAsync(PublicationOfEmailConfirmationModel publicationOfEmailConfirmationModel);
    public Task<UserReadModel> VerifyEmail(VerifyEmailModel verifyEmailModel);
    public Task<bool> DeleteUserById(Guid id);
    public Task<bool> CheckAvailableUsernameAsync(string username); 
    public Task<bool> CheckAvailableEmailAsync(string email);
    public Task<bool> ValidatePassword(ValidatePasswordModel validatePasswordModel);
}