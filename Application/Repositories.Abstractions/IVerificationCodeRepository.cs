using Services.Contracts;

namespace Repositories.Abstractions;
public interface IVerificationCodeRepository
{
    public Task<VerificationCodeModel?> GetCodeByUserIdAsync(Guid id, CancellationToken cancellationToken);
    public Task<bool> AddCodeAsync(VerificationCodeModel model, CancellationToken cancellationToken);
    Task<bool> DeleteCodeByIdAsync(Guid userId, CancellationToken cancellationToken);
}