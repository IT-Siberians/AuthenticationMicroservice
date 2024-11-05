using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Abstractions;
using Services.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TokenProvider;

public class JwtTokenGenerator(IOptions<JwtOptions> options) : IJwtTokenGenerator
{
    private readonly JwtOptions _options = options.Value;

    public async Task<TokenResult> GenerateAccessToken(UserModel model, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return null;
        }
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var identityClaims = new ClaimsIdentity(
        [
            new Claim(ClaimTypes.NameIdentifier, model.Id.ToString()),
            new Claim(ClaimTypes.Name, model.Username),
            new Claim(ClaimTypes.Email, model.Email),
            new Claim(ClaimTypes.Role, nameof(model.AccountStatus))
        ]);


        var tokenDescriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = credentials,
            Subject = identityClaims,
            Expires = DateTime.Now.AddMinutes(_options.AccessRefreshTokenExpiredTimePerMinutes)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenCookieName = _options.CookieName;
        var tokenResult = new TokenResult(tokenCookieName, tokenHandler.WriteToken(token));
        return tokenResult;
    }
    public async Task<TokenResult> GenerateRefreshToken(UserModel model, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return null;
        }
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var identityClaims = new ClaimsIdentity(
        [
            new Claim(ClaimTypes.NameIdentifier, model.Id.ToString()),
        ]);


        var tokenDescriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = credentials,
            Subject = identityClaims,
            Expires = DateTime.Now.AddMinutes(_options.RefreshTokenExpiredTimePerDays) // исправить на дни
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenCookieName = _options.CookieName;
        var tokenResult = new TokenResult(tokenCookieName, tokenHandler.WriteToken(token));
        return tokenResult;
    }

}