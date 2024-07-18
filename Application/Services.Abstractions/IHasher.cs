namespace Services.Abstractions;
public interface IHasher
{
    string GeneratePassword(string passwordToChange);
    public bool VerifyPassword(string password, string hashedPassword);
}