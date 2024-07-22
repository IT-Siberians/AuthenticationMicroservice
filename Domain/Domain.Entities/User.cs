using Domain.Entities.Enums;
using Domain.ValueObjects.ValueObjects;

namespace Domain.Entities;

public class User
{
    public Guid Id { get; }
    public Username Username { get; private set; }
    public PasswordHash PasswordHash { get; private set; }
    public Email Email { get; private set; }
    public AccountStatuses AccountStatus { get; private set; }

    protected User()
    {

    }

    public User(Username username, PasswordHash passwordHash, Email email)
    {
        Id = Guid.NewGuid();
        Username = username;
        PasswordHash = passwordHash;
        Email = email;
        AccountStatus = AccountStatuses.UnconfirmedAccount;
    }
    public void ChangeUsername(Username newUsername)
    {
        Username = newUsername;
    }

    public void ChangePassword(PasswordHash newPasswordHash)
    {
        PasswordHash = newPasswordHash;
    }

    private void ChangeEmail(Email newEmail)
    {
        Email = newEmail;
    }

    public void ConfirmNewEmail(Email newEmail)
    {
        ChangeEmail(newEmail);
        if (AccountStatus == AccountStatuses.UnconfirmedAccount)
        {
            AccountStatus = AccountStatuses.ConfirmedAccount;
        }
    }

    public bool SignIn()
    {
        return true;
    }

    public bool SignOut()
    {
        return true;
    }
}