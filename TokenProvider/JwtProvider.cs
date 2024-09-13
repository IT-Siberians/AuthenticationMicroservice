using Domain.Entities.Domain.Enums;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Abstractions;
using Services.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TokenProvider;
public class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
{
    private readonly JwtOptions _options = options.Value;

    public string GenerateTokenString(UserModel? user, TokenType tokenType)
    {
        Claim[] claims =
        [
            new Claim("userId", user.Id.ToString()),
            new Claim("AccountStatus", user.AccountStatus.ToString())
        ];

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = tokenType switch
        {
            TokenType.AccessToken => new JwtSecurityToken(claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_options.AccessTokenExpirationMinutes),
                signingCredentials: signingCredentials),
            TokenType.RefreshToken => new JwtSecurityToken(claims: claims,
                expires: DateTime.UtcNow.AddDays(_options.RefreshTokenExpirationDays),
                signingCredentials: signingCredentials),
            _ => throw new ArgumentOutOfRangeException(nameof(tokenType))
        };
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}