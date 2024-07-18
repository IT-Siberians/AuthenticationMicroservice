using Domain.ValueObjects.ValueObjects;

namespace Domain.Entities;

public class Guest(Username username, PasswordHash passwordHash, Email email)
{
    public User SignUp()
    {
        return new User(username, passwordHash, email);
    }
}