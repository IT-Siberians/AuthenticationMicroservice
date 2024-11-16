using Services.Contracts;

namespace Repositories.Abstractions;
public interface IVerificationCodeRepository
{
    public Task<VerificationCodeModel?> GetVerificationCodeByUserIdAsync(Guid id, CancellationToken cancellationToken);
    public Task<bool> AddVerificationCodeAsync(VerificationCodeModel model, CancellationToken cancellationToken);
}