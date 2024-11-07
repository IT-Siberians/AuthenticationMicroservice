using Services.Contracts;

namespace Repositories.Abstractions;
public interface ILinkIdRepository
{
    public Task<VerificationCodeModel?> GetLinkIdByUserIdAsync(Guid id, CancellationToken cancellationToken);
    public Task<bool> AddVerificationCodeAsync(VerificationCodeModel model, CancellationToken cancellationToken);
}