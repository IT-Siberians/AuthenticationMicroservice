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
    public async Task<IEnumerable<UserReadModel>> GetAllUsersAsync()
    {
        var users = await repository.GetAllAsync();
        return mapper.Map<IEnumerable<UserReadModel>>(users);
    }
    public async Task<UserReadModel> GetUserByIdAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentNullException(nameof(id));
        var user = await repository.GetByIdAsync(id);
        return mapper.Map<UserReadModel>(user);
    }

    public async Task<UserReadModel> CreateUserAsync(CreateUserModel createUserModel)
    {
        if (createUserModel == null ||
            string.IsNullOrWhiteSpace(createUserModel.Username) ||
            string.IsNullOrWhiteSpace(createUserModel.Password) ||
            string.IsNullOrWhiteSpace(createUserModel.Email))
            throw new ArgumentNullException(nameof(createUserModel));

        var isAvailableEmail = await repository.CheckIsAvailableEmailAsync(createUserModel.Email);
        if (!isAvailableEmail)
            throw new Exception("The email is already taken");//todo: найти лаконичное решение, сделать кастомные
        var isAvailableUsername = await repository.CheckIsAvailableUsernameAsync(createUserModel.Username);
        if (!isAvailableUsername)
            throw new Exception("The username is already taken");

        var guest = mapper.Map<Guest>(createUserModel);

        var user = guest.SignUp();
        var createdUser = await repository.AddAsync(user);
        if (createdUser == null)
            throw new Exception("The repository was unable to create an entity");// сделать кастомный эксепшн

        var publishCreatedUser = mapper.Map<PublicationOfEmailConfirmationModel>(createdUser);
        await publisher.PublishNewEmail(publishCreatedUser);

        return mapper.Map<UserReadModel>(user);
    }

    public async Task<UserReadModel> ChangeUsernameAsync(ChangeUsernameModel changeUsernameModel)
    {
        if (changeUsernameModel == null ||
            changeUsernameModel.Id == Guid.Empty ||
            string.IsNullOrWhiteSpace(changeUsernameModel.Username))
            throw new ArgumentNullException(nameof(changeUsernameModel));

        var isAvailableUsername = await repository.CheckIsAvailableUsernameAsync(changeUsernameModel.Username);
        if (!isAvailableUsername)
            throw new Exception("The username is already taken");

        var user = await repository.GetByIdAsync(changeUsernameModel.Id);
        if (user == null)
            throw new Exception("The entity to change was not found in the repository");

        user.ChangeUsername(changeUsernameModel.Username);

        var updatedUser = await repository.UpdateAsync(changeUsernameModel.Id, user);
        return mapper.Map<UserReadModel>(updatedUser);
    }

    public async Task<UserReadModel> ChangePasswordAsync(ChangePasswordModel changePasswordModel)
    {
        if (changePasswordModel == null ||
            changePasswordModel.Id == Guid.Empty ||
            string.IsNullOrWhiteSpace(changePasswordModel.Password) ||
            string.IsNullOrWhiteSpace(changePasswordModel.NewPassword))
            throw new ArgumentNullException(nameof(changePasswordModel));

        var user = await repository.GetByIdAsync(changePasswordModel.Id);
        if (user == null)
            throw new Exception("The entity to change was not found in the repository");

        var isValidPassword = hasher.VerifyHashedPassword(changePasswordModel.Password, user.PasswordHash.Value);
        if (!isValidPassword)
            throw new Exception("Invalid password");

        var newPassword = hasher.GenerateHashPassword(changePasswordModel.NewPassword);
        user.ChangePassword(newPassword);

        var updatedUser = await repository.UpdateAsync(changePasswordModel.Id, user);
        return mapper.Map<UserReadModel>(updatedUser);
    }

    public async Task<bool> CreateEmailChangeRequestAsync(PublicationOfEmailConfirmationModel publicationOfEmailConfirmationModel)
    {
        if (publicationOfEmailConfirmationModel == null ||
            publicationOfEmailConfirmationModel.Id == Guid.Empty ||
            string.IsNullOrWhiteSpace(publicationOfEmailConfirmationModel.NewEmail))
            throw new ArgumentNullException(nameof(publicationOfEmailConfirmationModel));

        var isAvailableEmail = await repository.CheckIsAvailableEmailAsync(publicationOfEmailConfirmationModel.NewEmail);
        if (!isAvailableEmail)
            throw new Exception("The email is already taken");

        var user = await repository.GetByIdAsync(publicationOfEmailConfirmationModel.Id);
        if (user == null)
            throw new Exception("The entity to change was not found in the repository");

        await publisher.PublishNewEmail(publicationOfEmailConfirmationModel);

        return true;
    }

    public async Task<UserReadModel> VerifyEmail(VerifyEmailModel verifyEmailModel)
    {
        if (verifyEmailModel == null ||
            verifyEmailModel.Id == Guid.Empty ||
            string.IsNullOrWhiteSpace(verifyEmailModel.NewEmail))
            throw new ArgumentNullException(nameof(verifyEmailModel));

        var isAvailableEmail = await repository.CheckIsAvailableEmailAsync(verifyEmailModel.NewEmail);
        if (!isAvailableEmail)
            throw new Exception("The email is already taken");

        var user = await repository.GetByIdAsync(verifyEmailModel.Id);
        if (user == null)
            throw new Exception("The entity to change was not found in the repository");

        user.ConfirmNewEmail(verifyEmailModel.NewEmail);

        var updatedUser = await repository.UpdateAsync(verifyEmailModel.Id, user);
        return mapper.Map<UserReadModel>(updatedUser);
    }

    public async Task<bool> DeleteUserById(Guid id)
    {
        return await repository.DeleteAsync(id);
    }
}