using Services.Abstractions;
using Services.Contracts;

namespace Services.Implementations;
public class TokenService(IJwtTokenGenerator generator) : ITokenService
{
    public Task<TokenResult> GetTokenAsync(UserModel user, CancellationToken cancellationToken)
    {
        return generator.GenerateAccessToken(user, cancellationToken);
    }
}