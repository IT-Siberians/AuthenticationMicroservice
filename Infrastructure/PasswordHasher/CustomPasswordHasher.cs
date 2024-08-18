using Services.Abstractions;
using BCrypt.Net;

namespace PasswordHasher;

public class CustomPasswordHasher : IPasswordHasher
{
    private const HashType HashType = BCrypt.Net.HashType.SHA512; // указываю явно, потому что только одна
                                                                  // перегрузка HashPassword предлагает выбрать способ шифрования
    private const bool EnhancedEntropy = true; // указываю явно, потому что только одна
                                               // перегрузка HashPassword предлагает выбрать способ шифрования
    private const int WorkFactor = 12;// указываю явно, потому что только одна
                                      // перегрузка HashPassword предлагает выбрать способ шифрования
    public string GenerateHashPassword(string password)
    {
        var salt = BCrypt.Net.BCrypt.GenerateSalt(WorkFactor); // указываю явно, потому что только одна
                                                               // перегрузка HashPassword предлагает выбрать способ шифрования

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt, EnhancedEntropy, HashType);

        return hashedPassword;
    }

    public bool VerifyHashedPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword, EnhancedEntropy, HashType);
    }
}