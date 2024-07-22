using Services.Contracts;

namespace Services.Abstractions;

public interface IAuthenticateService
{
    public Task<bool> AuthenticateUser(SignInDto signInDto);
    public Task<bool> LogoutUser(SignOutDto  signOutDto);
}
