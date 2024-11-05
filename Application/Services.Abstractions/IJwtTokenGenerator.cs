using Services.Contracts;

namespace Services.Abstractions;

public interface IJwtTokenGenerator
{
    Task<TokenResult> GenerateAccessToken(UserModel model, CancellationToken cancellationToken);
    Task<TokenResult> GenerateRefreshToken(UserModel model, CancellationToken cancellationToken);
}