using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Abstractions;
using Services.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmailTokenGenerator;

/// <summary>
/// Сервис создания токена подтверждения Email
/// </summary>
/// <param name="options">Опции сервиса</param>
public class EmailTokenService(IOptions<EmailTokenOptions> options) : IEmailTokenService
{
    private readonly EmailTokenOptions _options = options.Value;

    /// <summary>
    /// Создание/шифрование токена подтверждения Email
    /// </summary>
    /// <param name="model">Модель генерации подтверждения</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Токен подтверждения Email</returns>
    public async Task<string> EncryptAsync(MailConfirmationGenerationModel model,
        CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return default;

        var claims = new[]
        {
            new Claim("Id", model.Id.ToString()),
            new Claim("NewEmail", model.NewEmail),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: credentials,
            expires: DateTime.UtcNow.AddMinutes(_options.LifetimeInMinutes));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    /// <summary>
    /// Расшифровка/преобразование зашифрованного токена в модель установки Email
    /// </summary>
    /// <param name="token">Зашифрованный токен</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Модель установки Email</returns>
    public async Task<SetUserEmailModel> DecryptAsync(string token, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return default;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_options.Key);

        tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false
        }, out var validatedToken);

        var jwtToken = validatedToken as JwtSecurityToken;
        var id = Guid.Parse(jwtToken.Claims.First(x => x.Type == "Id").Value);
        var newEmail = jwtToken.Claims.First(x => x.Type == "NewEmail").Value;
        var expirationUnixTime = jwtToken.Payload.Expiration;
        var expirationTime = DateTimeOffset.FromUnixTimeSeconds((long)expirationUnixTime!).UtcDateTime;

        return new SetUserEmailModel()
        {
            Id = id,
            NewEmail = newEmail,
            ExpirationDateTime = expirationTime
        };
    }
}