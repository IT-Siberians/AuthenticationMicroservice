using Services.Contracts;

namespace Services.Abstractions;

public interface ITokenService
{
    Task<TokenResult> GetTokenAsync(UserModel user, CancellationToken cancellationToken);
}