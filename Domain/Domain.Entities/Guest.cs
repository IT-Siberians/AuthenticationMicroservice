using Domain.ValueObjects.ValueObjects;

namespace Domain.Entities;

public class Guest(Username username, PasswordHash passwordHash, Email email)
{
    public readonly Username Username = username;
    private readonly PasswordHash _passwordHash = passwordHash;
    public readonly Email Email = email;

    public User SignUp()
    {
        return new User(Username, _passwordHash, Email);
    }
}