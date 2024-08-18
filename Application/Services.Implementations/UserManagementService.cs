using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects.ValueObjects;
using Repositories.Abstractions;
using Services.Abstractions;
using Services.Contracts;

namespace Services.Implementations;

public class UserManagementService(
    IUserRepository repository,
    IMapper mapper,
    IPasswordHasher hasher) : IUserManagementService
{

    public async Task<IEnumerable<UserReadModel>> GetAllUsersAsync()
    {
        var users = await repository.GetAllAsync();
        return mapper.Map<IEnumerable<UserReadModel>>(users);
    }

    public async Task<UserReadModel> GetUserByIdAsync(Guid id)
    {
        var user = await repository.GetByIdAsync(id);
        return mapper.Map<UserReadModel>(user);
    }

    public async Task<UserReadModel> CreateUserAsync(CreateUserModel createUserModel)
    {
        var guest = mapper.Map<Guest>(createUserModel);

        var user = guest.SignUp();
        var createdUser = await repository.AddAsync(user);
        if (createdUser == null)
            throw new Exception("The repository was unable to create an entity");

        return mapper.Map<UserReadModel>(user);
    }

    public async Task<UserReadModel> ChangeUsernameAsync(ChangeUsernameModel changeUsernameModel)
    {
        var user = await repository.GetByIdAsync(changeUsernameModel.Id);
        if (user is null)
            throw new ArgumentNullException(nameof(changeUsernameModel)); // Вопрос уместно ли здесь исключение или лучше вернуть false?
                                                                          // Сама ситуация исключительна
                                                                          // так как перед обновлением я проверяю что пользователь найден
        user.ChangeUsername(changeUsernameModel.Username);

        var updatedUser = await repository.UpdateAsync(changeUsernameModel.Id, user);
        return mapper.Map<UserReadModel>(updatedUser);
    }

    public async Task<UserReadModel> ChangePasswordAsync(ChangePasswordModel changePasswordModel)
    {
        var user = await repository.GetByIdAsync(changePasswordModel.Id);
        if (user is null)
            throw new ArgumentNullException(nameof(changePasswordModel)); // Вопрос уместно ли здесь исключение или лучше вернуть false?
                                                                          // Сама ситуация исключительна
                                                                          // так как перед обновлением я проверяю что пользователь найден
        var newPassword = hasher.GenerateHashPassword(changePasswordModel.NewPassword);
        user.ChangePassword(newPassword);

        var updatedUser = await repository.UpdateAsync(changePasswordModel.Id, user);
        return mapper.Map<UserReadModel>(updatedUser);
    }

    public async Task<bool> CreateEmailChangeRequestAsync(
        PublicationOfEmailConfirmationModel publicationOfEmailConfirmationModel)
    {
        var user = await repository.GetByIdAsync(publicationOfEmailConfirmationModel.Id);
        if (user == null)
            return false;

        var newEmail = new Email(publicationOfEmailConfirmationModel.NewEmail); //здесь это нужно чтобы провалидировать эмейл
        //здесь будет логика отправки в почту

        return true;
    }

    public async Task<UserReadModel> VerifyEmail(VerifyEmailModel verifyEmailModel)
    {
        var user = await repository.GetByIdAsync(verifyEmailModel.Id);
        if (user is null)
            throw new ArgumentNullException(nameof(verifyEmailModel)); // Вопрос уместно ли здесь исключение или лучше вернуть false?
                                                                       // Сама ситуация исключительна
                                                                       // так как перед обновлением я проверяю что пользователь найден
        user.ConfirmNewEmail(verifyEmailModel.NewEmail);

        var updatedUser = await repository.UpdateAsync(verifyEmailModel.Id, user);
        return mapper.Map<UserReadModel>(updatedUser);
    }

    public async Task<bool> DeleteUserById(Guid id)
    {
        return await repository.DeleteAsync(id);
    }

    public async Task<bool> CheckAvailableUsernameAsync(string username)
    {
        return await repository.CheckIsAvailableUsernameAsync(username);
    }

    public async Task<bool> CheckAvailableEmailAsync(string email)
    {
        return await repository.CheckIsAvailableEmailAsync(email);

    }

    public async Task<bool> ValidatePassword(ValidatePasswordModel validatePasswordModel)
    {
        var user = await repository.GetByIdAsync(validatePasswordModel.Id);
        if (user == null)
            return false;

        return hasher.VerifyHashedPassword(validatePasswordModel.Password, user.PasswordHash.Value);
    }
}