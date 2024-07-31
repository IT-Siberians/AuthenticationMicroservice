using Services.Abstractions;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace PasswordHasher;
public class CustomPasswordHasher : IPasswordHasher
{
    private const int SaltSize = 16;
    private const int Iterations = 10000;
    private const int KeySize = 32;
    public string GenerateHashPassword(string? password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentNullException(nameof(password));
        byte[] salt = new byte[SaltSize];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        byte[] hash = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA512, Iterations, KeySize);

        return $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
    }

    public bool VerifyHashedPassword(string password, string hashedPassword)
    {
        string[] parts = hashedPassword.Split('.', 2);
        if (parts.Length != 2)
        {
            return false;
        }

        byte[] salt = Convert.FromBase64String(parts[0]);
        byte[] hash = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA512, Iterations, KeySize);

        return parts[1].Equals(Convert.ToBase64String(hash));
    }
}