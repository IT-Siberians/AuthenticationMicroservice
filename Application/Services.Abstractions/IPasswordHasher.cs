namespace Services.Abstractions;

public interface IPasswordHasher
{
    public string GenerateHashPassword(string password);
    public bool VerifyHashedPassword(string password, string hashedPassword);
}